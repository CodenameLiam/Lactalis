
using System.ComponentModel.DataAnnotations;



namespace APITests.Settings
{
	public class UserSettings
	{
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

	}
}