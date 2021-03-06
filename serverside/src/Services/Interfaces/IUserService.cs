
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Lactalis.Exceptions;
using Lactalis.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


namespace Lactalis.Services.Interfaces
{
	public interface IUserService
	{
		/// <summary>
		/// Check the username and password of a user.
		/// </summary>
		/// <param name="username">The username of the user</param>
		/// <param name="password">The password of the user</param>
		/// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails</param>
		/// <param name="validateEmailConfirmation">Weather the login should check if the email has been confirmed</param>
		/// <returns>On success returns the user object, on failure throws an exception</returns>
		/// <exception cref="InvalidUserPasswordException">On the username or password being invalid</exception>
		Task<User> CheckCredentials(string username, string password, bool lockoutOnFailure = true, bool validateEmailConfirmation = true);

		/// <summary>
		/// Confirm an email for a user
		/// </summary>
		/// <param name="email">The email to confirm</param>
		/// <param name="token">The token to confirm the email with</param>
		/// <returns>An identity result identifying the success of the operation</returns>
		Task<IdentityResult> ConfirmEmail(string email, string token);

		/// <summary>
		/// Creates a ClaimsPrincipal using the specified authentication type
		/// </summary>
		/// <param name="user">The user to create the claims principal for</param>
		/// <param name="authenticationScheme">The authentication scheme used, cookie by default</param>
		/// <returns>The claims principal for the user</returns>
		Task<ClaimsPrincipal> CreateUserPrincipal(User user, string authenticationScheme = "Cookies");

		/// <summary>
		/// Deletes a user
		/// </summary>
		/// <param name="id">The id of the user to delete</param>
		/// <returns>Task containing boolean indicating success</returns>
		Task<bool> DeleteUser(Guid id);


		/// <summary>
		/// Creates a authentication ticket to identify a user
		/// </summary>
		/// <param name="request">The OpenId request for the user</param>
		/// <returns>The authentication ticket for the user</returns>
		/// <exception cref="InvalidUserPasswordException">Thrown when an invalid username or password is provided</exception>
		/// <exception cref="InvalidGrantTypeException">Thrown when an invalid OpenId grant type is provided</exception>
		Task<AuthenticationTicket> Exchange(OpenIdConnectRequest request);

		/// <summary>
		/// Gets a user result from a given claims principal
		/// </summary>
		/// <param name="principal">The claims principal to extract the user from</param>
		/// <returns>The user that is provided by the principal</returns>
		/// <exception cref="InvalidIdException">When the principal does not apply to a valid user</exception>
		Task<UserResult> GetUser(ClaimsPrincipal principal);

		/// <summary>
		/// Gets a user result from a given user
		/// </summary>
		/// <param name="user">The user to make the user result from</param>
		/// <returns>A user result</returns>
		Task<UserResult> GetUser(User user);

		/// <summary>
		/// Gets a user from a claims principal based off of the username
		/// </summary>
		/// <param name="principal">The claims principal to get the credentials from</param>
		/// <returns>A user, or null if one is not found</returns>
		Task<User> GetUserFromClaim(ClaimsPrincipal principal);

		/// <summary>
		/// Gets all users
		/// </summary>
		/// <returns>A task that resolves to a list of all users</returns>
		Task<List<UserResult>> GetUsers();

		/// <summary>
		/// Registers a new user given a registration model and a list of groups
		/// </summary>
		/// <param name="model">The registration model of the user to create</param>
		/// <param name="groups">The groups that the user shall be added to</param>
		/// <param name="sendRegisterEmail">
		/// Should an email be sent to validate the users email. If this is set to true then the user will not be
		/// immediately activated, otherwise no email will be sent and the user will be immediately activated.
		/// </param>
		/// <returns>An identity result indicating the success of the operation</returns>
		/// <exception cref="DuplicateUserException">On a user with this email already existing</exception>
		Task<RegisterResult> RegisterUser(RegisterModel model, IEnumerable<string> groups, bool sendRegisterEmail = false);

		/// <summary>
		/// Registers a new user given a user model, password and list of groups
		/// </summary>
		/// <param name="user">The user model of the user to create</param>
		/// <param name="password">The password of the user</param>
		/// <param name="groups">The groups that the user shall be added to</param>
		/// <param name="sendRegisterEmail">
		/// Should an email be sent to validate the users email. If this is set to true then the user will not be
		/// immediately activated, otherwise no email will be sent and the user will be immediately activated.
		/// </param>
		/// <returns>An identity result indicating the success of the operation</returns>
		Task<RegisterResult> RegisterUser(User user, string password, IEnumerable<string> groups, bool sendRegisterEmail = false);

		/// <summary>
		/// Sends a reset password email to a user
		/// </summary>
		/// <param name="user">The user to reset the password of</param>
		/// <returns>True if the email was successfully sent</returns>
		Task<bool> SendPasswordResetEmail(User user);

		/// <summary>
		/// updates a new user
		/// </summary>
		/// <param name="model">The registration model of the user to create</param>
		/// <returns>An identity result indicating the success of the operation</returns>
		/// <exception cref="DuplicateUserException">On a user with this email already existing</exception>
		Task<IdentityResult> UpdateUser(UserUpdateModel model);

	}
}