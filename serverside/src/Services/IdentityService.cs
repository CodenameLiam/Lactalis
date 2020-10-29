
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Services.Interfaces;
using Lactalis.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Lactalis.Services
{
	public class IdentityService : IIdentityService
	{
		public bool Fetched { get; set; } = false;

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;

		/// <inheritdoc />
		public User User { get; set; }

		/// <inheritdoc />
		public IList<string> Groups { get; set; }


		public IdentityService(
			IHttpContextAccessor httpContextAccessor,
			IUserService userService,
			UserManager<User> userManager)
		{
			_httpContextAccessor = httpContextAccessor;
			_userService = userService;
			_userManager = userManager;
		}

		/// <inheritdoc />
		public async Task RetrieveUserAsync()
		{
			if (Fetched != true)
			{
				User = await _userService.GetUserFromClaim(_httpContextAccessor.HttpContext.User);
				Groups = User == null ? new List<string>() : await _userManager.GetRolesAsync(User);
				Groups.AddRange(SecurityUtilities.GetAllAcls()
					.Where(x => x.IsVisitorAcl && x.Group != null)
					.Select(x => x.Group)
					.ToHashSet());
				Fetched = true;
			}
		}

	}
}