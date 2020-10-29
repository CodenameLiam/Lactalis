

using Lactalis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace ServersideTests.Mocks
{
	public class MockRoleManager : Mock<RoleManager<Group>>
	{

		public MockRoleManager(
			IRoleStore<Group> store,
			IEnumerable<IRoleValidator<Group>> roleValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			ILogger<RoleManager<Group>> logger

		) :
		base(
			store,
			roleValidators,
			keyNormalizer,
			errors,
			logger
		)
		{
		}

		public static MockRoleManager GetMockRoleManager(
			IRoleStore<Group> store = null,
			IEnumerable<IRoleValidator<Group>> roleValidators = null,
			ILookupNormalizer keyNormalizer = null,
			IdentityErrorDescriber errors = null,
			ILogger<RoleManager<Group>> logger = null)
		{
			return new  MockRoleManager(
 				store ?? new Mock<IRoleStore<Group>>().Object,
				roleValidators,
				keyNormalizer,
				errors,
				logger);
		}
	}
}