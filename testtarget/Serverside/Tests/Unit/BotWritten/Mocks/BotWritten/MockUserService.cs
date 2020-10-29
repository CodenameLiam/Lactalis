

using System;
using Lactalis.Models;
using Lactalis.Services;
using Lactalis.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ServersideTests.Mocks
{
	public class MockUserService: Mock<UserService>
	{
		public MockUserService(
			IOptions<IdentityOptions> identityOptions,
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			IHttpContextAccessor httpContextAccessor,
			RoleManager<Group> roleManager,
			IEmailService emailService,
			IConfiguration configuration) :
			base(
				identityOptions,
				signInManager,
				userManager,
				httpContextAccessor,
				roleManager,
				emailService,
				configuration)
		{

		}

		public static MockUserService GetMockUserService(
			IOptions<IdentityOptions> identityOptions = null,
			SignInManager<User> signInManager = null,
			UserManager<User> userManager = null,
			IHttpContextAccessor httpContextAccessor = null,
			RoleManager<Group> roleManager = null,
			IEmailService emailService = null,
			IConfiguration configuration = null)
		{
			return new MockUserService(
				identityOptions ?? new Mock<IOptions<IdentityOptions>>().Object,
				signInManager ?? MockSignInManager.GetMockSignInManager(userManager: userManager ?? MockUserManager.GetMockUserManager().Object).Object,
				userManager ?? MockUserManager.GetMockUserManager().Object,
				httpContextAccessor ?? new Mock<IHttpContextAccessor>().Object,
				roleManager ?? MockRoleManager.GetMockRoleManager().Object,
				emailService ?? new Mock<IEmailService>().Object,
				configuration ?? new Mock<IConfiguration>().Object);
		}
	}
}