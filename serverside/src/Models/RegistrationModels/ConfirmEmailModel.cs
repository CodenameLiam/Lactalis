
using System.ComponentModel.DataAnnotations;

namespace Lactalis.Models.RegistrationModels
{
	public class ConfirmEmailModel
	{
		/// <summary>
		/// The email to confirm
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// The confirmation token to confirm the email with
		/// </summary>
		[Required]
		public string Token { get; set; }
	}
}