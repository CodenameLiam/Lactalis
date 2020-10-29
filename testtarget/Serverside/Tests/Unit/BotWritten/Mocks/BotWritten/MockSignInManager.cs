

using Lactalis.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ServersideTests.Mocks
{
	public class MockSignInManager : Mock<SignInManager<User>>
	{

		public MockSignInManager(
			UserManager<User> userManager,
			IHttpContextAccessor httpContext,
			IUserClaimsPrincipalFactory<User> claimsPrincipalFactory,
			IOptions<IdentityOptions> options,
			ILogger<SignInManager<User>> logger,
			IAuthenticationSchemeProvider authenticationScheme,
			IUserConfirmation<User> userConfirmation
		) :
			base(
			userManager,
			httpContext,
			claimsPrincipalFactory,
			options,
			logger,
			authenticationScheme,
			userConfirmation)
		{
		}

		public static MockSignInManager GetMockSignInManager(
			UserManager<User> userManager = null,
			IHttpContextAccessor httpContext = null,
			IUserClaimsPrincipalFactory<User> claimsPrincipalFactory = null,
			IOptions<IdentityOptions> options = null,
			ILogger<SignInManager<User>> logger = null,
			IAuthenticationSchemeProvider authenticationScheme = null,
			IUserConfirmation<User> userConfirmation = null
			)
		{
			return new  MockSignInManager(
				userManager ?? MockUserManager.GetMockUserManager().Object,
				httpContext ?? new Mock<IHttpContextAccessor>().Object,
				claimsPrincipalFactory ?? new Mock<IUserClaimsPrincipalFactory<User>>().Object,
				options ?? new Mock<IOptions<IdentityOptions>>().Object,
				logger ?? new Mock<ILogger<SignInManager<User>>>().Object,
				authenticationScheme ?? new Mock<IAuthenticationSchemeProvider>().Object,
				userConfirmation ?? new Mock<IUserConfirmation<User>>().Object);
		}
	}
}