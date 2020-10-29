

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Lactalis.Exceptions;
using Lactalis.Models;
using Lactalis.Services;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using ServersideTests.Mocks;
using TestDataLib;
using Xunit;



namespace ServersideTests.Tests.Unit.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class UserServiceTests
	{
		private readonly Fixture _fixture;
		private readonly User _testUser;
		private readonly MockUserManager _mockUserManager;
		private readonly Mock<RoleManager<Group>> _mockRoleManager;
		private readonly IList<string> _groupListNames;

		public UserServiceTests()
		{
			_fixture = new Fixture();

			// create a user by setting creating
			_testUser = new User
			{
				UserName = _fixture.Create("Username"),
				Email = DataUtils.RandEmail()
			};

			_mockUserManager = MockUserManager.GetMockUserManager();
			_mockRoleManager = MockRoleManager.GetMockRoleManager();


			var testGroupListName = _fixture.Create("Group");
			_groupListNames = new List<string> { testGroupListName };
		}

		/// <summary>
		/// Attempt to get a user
		/// </summary>
		[Fact]
		public async void UserServiceGetUserTest()
		{
			//arrange
			var testGroups = _groupListNames.Select(x => new Group { Name = x });
			var mockTestGroups = testGroups.AsQueryable().BuildMock();

			_mockUserManager.Setup(x => x.GetRolesAsync(_testUser)).Returns(Task.FromResult(_groupListNames));
			_mockRoleManager.Setup(x => x.Roles).Returns(mockTestGroups.Object);

			var userService =
				MockUserService.GetMockUserService(
					signInManager: MockSignInManager.GetMockSignInManager(userManager: _mockUserManager.Object).Object,
					userManager: _mockUserManager.Object,
					roleManager: _mockRoleManager.Object
				).Object;

			// act
			var result = await userService.GetUser(_testUser);

			// assert
			result.Groups
				.Should()
				.HaveCount(testGroups.Count());

			result.Groups.Select(x => x.Name).Should().Equal(_groupListNames);

			Assert.Equal(_testUser.UserName, result.Email);
		}

		/// <summary>
		/// Attempt to create a user which is already in the system,
		/// should return a duplicate user exception
		/// </summary>
		[Fact]
		public void UserServiceRegisterDuplicateUserTest()
		{
			// arrange
			// create a registration model with same email as the test user
			var registrationModel = new RegisterModel
			{
				Email = _testUser.Email,
				Password = _fixture.Create<string>(),
				Groups = _groupListNames
			};

			_mockUserManager
				.Setup(x => x.FindByEmailAsync(_testUser.Email))
				.Returns(Task.FromResult(_testUser));

			var userService =
				MockUserService.GetMockUserService(
					signInManager: MockSignInManager.GetMockSignInManager(userManager: _mockUserManager.Object).Object,
					userManager: _mockUserManager.Object,
					roleManager: _mockRoleManager.Object
				).Object;

			// act
			Func<Task> act = async () =>
				await userService.RegisterUser(registrationModel, _groupListNames);

			// assert
			act.Should().Throw<DuplicateUserException>();
		}

		/// <summary>
		/// Attempt to create a user successfully,
		/// should return a duplicate user exception
		/// </summary>
		[Fact]
		public async void UserServiceRegisterUserTest()
		{
			// arrange
			var mockedResult = new MockIdentityResult().MockResultSuccession(true);

			var testPassword = _fixture.Create<string>();

			// create a registration model with same email as the test user
			var registrationModel = new RegisterResult
			{
				Result = mockedResult,
				User = _testUser
			};

			_mockUserManager
				.Setup(x => x.CreateAsync(_testUser, testPassword))
				.Returns(Task.FromResult((IdentityResult)mockedResult));

			var usersList = new List<User> { _testUser };

			var mockUsers = usersList.AsQueryable().BuildMock();
			_mockUserManager.Setup(x => x.Users).Returns(mockUsers.Object);

			var userService =
				MockUserService.GetMockUserService(
					signInManager: MockSignInManager.GetMockSignInManager(userManager: _mockUserManager.Object).Object,
					userManager: _mockUserManager.Object,
					roleManager: _mockRoleManager.Object
				).Object;

			// act
			var result = await userService.RegisterUser(_testUser, testPassword, _groupListNames);

			// assert
			registrationModel.ToExpectedObject().ShouldMatch(result);
		}
	
	}
}