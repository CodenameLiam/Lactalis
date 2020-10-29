
using System;
using System.Collections.Generic;
using Lactalis.Models;
using Lactalis.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;


namespace Lactalis.Services
{
	public class AuditService : IAuditService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		/// <inheritdoc />
		public List<AuditLog> Logs { get; } = new List<AuditLog>();

		public AuditService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_httpContextAccessor?.HttpContext?.Items.TryAdd("AuditLogs", Logs);
		}

		/// <inheritdoc />
		public void CreateReadAudit(string userId, string userName, string modelName, object data = null)
		{
			var audit = new AuditLog
			{
				UserName = userName,
				UserId = userId,
				Id = Guid.NewGuid(),
				Action = "Read",
				TablePk = null,
				AuditData = data == null ? null : JObject.FromObject(data),
				AuditDate = DateTime.UtcNow,
				EntityType = modelName,
				HttpContextId = _httpContextAccessor?.HttpContext?.TraceIdentifier
			};

			Logs.Add(audit);
		}
	}
}