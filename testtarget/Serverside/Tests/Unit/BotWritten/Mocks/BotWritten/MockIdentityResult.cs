

using Microsoft.AspNetCore.Identity;

namespace ServersideTests.Mocks
{
	public class MockIdentityResult : IdentityResult
	{
		public MockIdentityResult MockResultSuccession(bool succeeded)
		{
			Succeeded = succeeded;
			return this;
		}
	}
}