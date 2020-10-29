

using System;
using System.Linq;
using APITests.Setup;
using Lactalis.Models;

namespace APITests.Utils
{
	internal static class UserHelper
	{
		public static User GetUserFromDB(Guid id)
		{
			using var context = new LactalisDBContext(new StartupTestFixture().DbContextOptions, null, null);
			return context.Users.FirstOrDefault(u => u.Id == id);
		}
	}
}
