
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lactalis.Exceptions;
using Lactalis.Models;
using Lactalis.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
 

namespace Lactalis.Helpers
{
	public class SeedGroup
	{
		public string Name { get; set; }
		public bool HasBackendAccess { get; set; }
	}

	/// <summary>
	/// Seeds groups for all environments and users for development environments
	/// </summary>
	public class DataSeedHelper
	{
		private readonly RoleManager<Group> _roleManager;
		private readonly IUserService _userService;
		private readonly ILogger<DataSeedHelper> _logger;
		private readonly IWebHostEnvironment _environment;

		private static readonly List<SeedGroup> Roles = new List<SeedGroup>
		{
			// Super administrators has no special access but just allows the super dev account to open to the admin
			// pages without there being a modelled admin group.
			new SeedGroup {Name = "Super Administrators", HasBackendAccess = true},
			new SeedGroup {Name = "Visitors", HasBackendAccess = false},
			new SeedGroup {Name = "Admin", HasBackendAccess = false},
			new SeedGroup {Name = "Farmer", HasBackendAccess = false},

		};


		public DataSeedHelper(
			RoleManager<Group> roleManager,
			IUserService userService,
			ILogger<DataSeedHelper> logger,
			IWebHostEnvironment environment
			)
		{
			_roleManager = roleManager;
			_userService = userService;
			_logger = logger;
			_environment = environment;
		}

		public void Initialize()
		{

			Task.WaitAll(CreateObjects());

		}

		private async Task CreateObjects()
		{
			// Create the roles first since we need them to assign users to afterwards
			foreach (var role in Roles)
			{
				await CreateRole(role);
			}

			if (_environment.IsDevelopment())
			{
				// Create users for testing in development environments
				await CreateUser(
					new User {Email = "super@example.com", Discriminator = "User"},
					"password",
					new [] {"Visitors", "Admin", "Farmer", "Super Administrators"});
				await CreateUser(
					new AdminEntity {Email = "admin@example.com"},
					"password",
					new [] {"Admin"});
				await CreateUser(
					new FarmerEntity {Email = "farmer@example.com"},
					"password",
					new [] {"Farmer"});
			}

		}

		private async Task CreateRole(SeedGroup seedGroup)
		{
			var group = await _roleManager.FindByNameAsync(seedGroup.Name);
			if (group == null)
			{
				await _roleManager.CreateAsync(new Group
				{
					Id = Guid.NewGuid(),
					Name = seedGroup.Name,
					HasBackendAccess = seedGroup.HasBackendAccess,
				});
			}
			else
			{
				if (group.HasBackendAccess != seedGroup.HasBackendAccess)
				{
					group.HasBackendAccess = seedGroup.HasBackendAccess;
					await _roleManager.UpdateAsync(group);
				}


				_logger.LogInformation("Not creating group {GroupName} since this group already exists", seedGroup.Name, seedGroup);
			}
		}

		private async Task CreateUser(
			User user,
			string password,
			IEnumerable<string> groups,
			bool sendRegisterEmail = false)
		{
			try
			{
				var result = await _userService.RegisterUser(user, password, groups, sendRegisterEmail);
				if (!result.Result.Succeeded)
				{
					var duplicateUserNameError = result.Result.Errors.FirstOrDefault(e => e.Code == "DuplicateUserName");
					if (duplicateUserNameError == null)
					{
						throw new AggregateException(result.Result.Errors.Select(e => new Exception(e.Description)));
					}
					_logger.LogInformation("Not creating user {SeedUserName} since this user already exists", user.Email);
				}
			}
			catch (DuplicateUserException)
			{
				_logger.LogInformation("Not creating user {SeedUserName} since this user already exists", user.Email);
			}
			catch (DbUpdateException e)
			{
				_logger.LogError(e, "Not creating user {SeedUserName} because of a database error", user.Email);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Unable to create {SeedUserName} because of an unhandled error", user.Email);
			}
		}

	}
}
