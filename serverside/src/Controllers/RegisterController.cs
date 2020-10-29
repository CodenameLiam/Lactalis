
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lactalis.Exceptions;
using Lactalis.Models;
using Lactalis.Models.RegistrationModels;
using Lactalis.Services;
using Lactalis.Services.Interfaces;
using Lactalis.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
 

namespace Lactalis.Controllers
{
	[ApiController]
	[Authorize]
	[Route("/api/register")]
	public class RegisterController : Controller
	{
		private readonly IUserService _userService;
		private readonly ILogger<RegisterController> _logger;

		public RegisterController(
			IUserService userService,
			ILogger<RegisterController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		/// <summary>
		/// Registers a new Admin Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("admin-entity")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RegisterAdminEntity([FromBody] AdminEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}

		/// <summary>
		/// Registers a new Farmer Entity user
		/// </summary>
		/// <param name="registrationModel">The fields to set on the user</param>
		/// <returns>A user result on success or a list of errors on failure</returns>
		[HttpPost]
		[Route("farmer-entity")]
		[Authorize(Roles = "Admin,Farmer")]
		public async Task<IActionResult> RegisterFarmerEntity([FromBody] FarmerEntityRegistrationModel registrationModel)
		{
			var model = registrationModel.ToModel();
			return await Register(model, registrationModel.Password, registrationModel.Groups);
		}


		[HttpPost]
		[Route("confirm-email")]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
		{
			var result = await _userService.ConfirmEmail(model.Email, model.Token);

			if (!result.Succeeded)
			{
				_logger.LogInformation("User confirm email validation failed for {Email}", model.Email);
				_logger.LogInformation(JsonConvert.SerializeObject(result));
				return Unauthorized();
			}

			return Ok();
		}

		private async Task<IActionResult> Register(User model, string password, IEnumerable<string> groups)
		{
			try
			{
				var result = await _userService.RegisterUser(model, password, groups, true);

				if (result.Result.Succeeded == false)
				{
					_logger.LogInformation("Failed to create user {Email}", model.Email);
					_logger.LogInformation(JsonConvert.SerializeObject(result.Result));
					return BadRequest(new ApiErrorResponse(result.Result.Errors.Select(e => e.Description)));
				}

				var userResponse = await _userService.GetUser(result.User);
				return Ok(userResponse);
			}
			catch (DuplicateUserException e)
			{
				_logger.LogInformation("Attempted to create duplicate user. Email: {Email}", model.Email);
				// In the case of a duplicate user return a 409 Conflict response code
				return StatusCode(StatusCodes.Status409Conflict, new ApiErrorResponse(e.Message));
			}
		}

	}

}