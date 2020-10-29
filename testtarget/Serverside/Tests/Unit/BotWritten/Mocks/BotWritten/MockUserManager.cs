

using System;
using Lactalis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ServersideTests.Mocks
{
	public class MockUserManager : Mock<UserManager<User>>
	{
		public MockUserManager(
			IUserStore<User> userStore,
			IOptions<IdentityOptions> identityOptions,
			IPasswordHasher<User> passwordHasher,
			IUserValidator<User>[] userValidator,
			IPasswordValidator<User>[] passwordValidator,
			ILookupNormalizer lookupNormalizer,
			IdentityErrorDescriber identityErrorDescriber,
			IServiceProvider serviceProvider,
			ILogger<UserManager<User>> logger) :
			base(
			userStore,
			identityOptions,
			passwordHasher,
			userValidator,
			passwordValidator,
			lookupNormalizer,
			identityErrorDescriber,
			serviceProvider,
			logger)
		{

		}

		public static MockUserManager GetMockUserManager(
			IUserStore<User> userStore = null,
			IOptions<IdentityOptions> identityOptions = null,
			IPasswordHasher<User> passwordHasher = null,
			IUserValidator<User>[] userValidator = null,
			IPasswordValidator<User>[] passwordValidator = null,
			ILookupNormalizer lookupNormalizer = null,
			IdentityErrorDescriber identityErrorDescriber = null,
			IServiceProvider serviceProvider = null,
			ILogger<UserManager<User>> logger = null)
		{
			return new MockUserManager(
				userStore ?? new Mock<IUserStore<User>>().Object,
				identityOptions ?? new Mock<IOptions<IdentityOptions>>().Object,
				passwordHasher ?? new Mock<IPasswordHasher<User>>().Object,
				userValidator ?? new IUserValidator<User>[0],
				passwordValidator ?? new IPasswordValidator<User>[0],
				lookupNormalizer ?? new Mock<ILookupNormalizer>().Object,
				identityErrorDescriber ?? new Mock<IdentityErrorDescriber>().Object,
				serviceProvider ?? new Mock<IServiceProvider>().Object,
				logger ?? new Mock<ILogger<UserManager<User>>>().Object);
		}
	}
}