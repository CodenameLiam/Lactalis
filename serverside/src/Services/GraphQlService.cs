
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Models;
using Lactalis.Services.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Lactalis.Services
{
	public class LactalisGraphQlContext
	{
		public LactalisDBContext DbContext { get; set; }
		public User User { get; set; }
		public IList<string> UserGroups { get; set; }
		public ISecurityService SecurityService { get; set; }
		public UserManager<User> UserManager { get; set; }
		public IUserService UserService { get; set; }
		public ICrudService CrudService { get; set; }
		public IIdentityService IdentityService { get; set; }
		public IServiceProvider ServiceProvider { get; set; }
		public IAuditService AuditService { get; set; }
		public IFormFileCollection Files { get; set; }
	}

	public class GraphQlService : IGraphQlService
	{
		private readonly IDocumentExecuter _executer;
		private readonly ISchema _schema;
		private readonly LactalisDBContext _dataContext;
		private readonly ISecurityService _securityService;
		private readonly UserManager<User> _userManager;
		private readonly IUserService _userService;
		private readonly ICrudService _crudService;
		private readonly IIdentityService _identityService;
		private readonly IServiceProvider _serviceProvider;
		private readonly IAuditService _auditService;

		public GraphQlService(
			ISchema schema,
			IDocumentExecuter executer,
			LactalisDBContext dataContext,
			ISecurityService securityService,
			UserManager<User> userManager,
			IUserService userService,
			ICrudService crudService,
			IServiceProvider serviceProvider,
			IIdentityService identityService,
			IAuditService auditService)
		{
			_schema = schema;
			_executer = executer;
			_dataContext = dataContext;
			_securityService = securityService;
			_userManager = userManager;
			_userService = userService;
			_crudService = crudService;
			_identityService = identityService;
			_serviceProvider = serviceProvider;
			_auditService = auditService;
		}

		/// <inheritdoc />
		public async Task<ExecutionResult> Execute(
			string query,
			string operationName,
			Inputs variables,
			IFormFileCollection attachments,
			User user,
			CancellationToken cancellation)
		{
			await _identityService.RetrieveUserAsync();

			var executionOptions = new ExecutionOptions
			{
				Schema = _schema,
				Query = query,
				OperationName = operationName,
				Inputs = variables,
				UserContext = new LactalisGraphQlContext
				{
					DbContext = _dataContext,
					User = user,
					UserGroups = _identityService.Groups,
					SecurityService = _securityService,
					CrudService = _crudService,
					IdentityService = _identityService,
					UserManager = _userManager,
					UserService = _userService,
					ServiceProvider = _serviceProvider,
					AuditService = _auditService,
					Files = attachments,
				},
				CancellationToken = cancellation,
#if (DEBUG)
				ExposeExceptions = true,
				EnableMetrics = true,
#endif
			};

			var result = await _executer.ExecuteAsync(executionOptions)
				.ConfigureAwait(false);

			return result;
		}
	}
}