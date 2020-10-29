
using System;
using System.IO;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace ServersideTests.Helpers
{
	public class ServerBuilderOptions
	{
		/// <summary>
		/// The name of the in memory database to use
		/// </summary>
		public string DatabaseName { get; set; } = Path.GetRandomFileName();

		/// <summary>
		/// Should the data seed helper be called to initialise data
		/// This will create the user super@example.com for testing
		/// </summary>
		public bool InitialiseData { get; set; } = true;

		/// <summary>
		/// The claims principal used to represent the testing user
		/// </summary>
		public ClaimsPrincipal UserPrincipal { get; set; } = ServerBuilder.CreateUserPrincipal(
			Guid.NewGuid(),
			"super@example.com",
			"super@example.com",
			new [] {"Visitors", "Admin", "Farmer", "Super Administrators"});

		/// <summary>
		/// Configuration function for the database for the tests
		/// </summary>
		public Func<ServerBuilderOptions, Action<DbContextOptionsBuilder>> DatabaseOptions { get; set; } = builderOptions => options =>
		{
			options.UseInMemoryDatabase(builderOptions.DatabaseName);
			options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
			options.UseOpenIddict<Guid>();
		};

		/// <summary>
		/// Configure any additional services for the tests
		/// </summary>
		public Action<IServiceCollection, ServerBuilderOptions> ConfigureServices { get; set; } = null;

	}
}