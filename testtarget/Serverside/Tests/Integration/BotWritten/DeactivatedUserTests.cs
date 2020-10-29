
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Lactalis.Controllers;
using Lactalis.Models;
using Xunit;

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class DeactivatedUserTests
	{
		[Fact]
		public async void AdminEntityDeactivatedLoginTest()
		{
			await CreateAndValidateUser<AdminEntity>();
		}

		[Fact]
		public async void FarmerEntityDeactivatedLoginTest()
		{
			await CreateAndValidateUser<FarmerEntity>();
		}


		private static async Task CreateAndValidateUser<T>()
			where T : User, new()
		{
			using var host = ServerBuilder.CreateServer();

			var controller = host.Services.GetRequiredService<AuthorizationController>();
			var userManager = host.Services.GetRequiredService<UserManager<User>>();

			// Create a user with the user manager
			var entity = new EntityFactory<T>()
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.First();

			var id = Guid.NewGuid().ToString();
			entity.UserName = id;
			entity.Email = $"{id}@example.com";
			entity.NormalizedUserName = entity.UserName.ToUpper();
			entity.NormalizedEmail = entity.Email.ToUpper();
			entity.EmailConfirmed = false;
			await userManager.CreateAsync(entity, "password");

			var result = await controller.Login(new LoginDetails
			{
				Username = entity.UserName,
				Password = "password"
			});

			Assert.Equal(typeof(UnauthorizedObjectResult), result.GetType());
		}
	}
}