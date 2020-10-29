
using APITests.Factories;
using Xunit;


namespace APITests.TheoryData.BotWritten
{
	public class PasswordInvalidTheoryData : TheoryData<UserEntityFactory, string, string>
	{
		public PasswordInvalidTheoryData()
		{
			var passwordMustContainDigitError = "Passwords must have at least one digit ('0'-'9').";
			var passwordMustContainUppercaseError = "Passwords must have at least one uppercase ('A'-'Z').";
			var passwordMustContainNonAlphanumericError = "Passwords must have at least one non alphanumeric character.";
			var passwordLengthError = "Passwords must be at least 6 characters.";


			Add(
				new UserEntityFactory("FarmerEntity"),
				"pass",
				passwordLengthError);
			Add(
				new UserEntityFactory("AdminEntity"),
				"pass",
				passwordLengthError);

		}
	}

	public class UsernameInvalidTheoryData : TheoryData<UserEntityFactory, string, string>
	{
		public UsernameInvalidTheoryData()
		{
			var InvalidEmailError = "Email is not a valid email";

			Add(
				new UserEntityFactory("FarmerEntity"),
				"super@example.com",
				"User name 'super@example.com' is already taken."
			);
			Add(
				new UserEntityFactory("FarmerEntity"),
				"super",
				InvalidEmailError
			);
			Add(
				new UserEntityFactory("FarmerEntity"),
				"@e.c*@example.com",
				InvalidEmailError
			);
			Add(
				new UserEntityFactory("AdminEntity"),
				"super@example.com",
				"User name 'super@example.com' is already taken."
			);
			Add(
				new UserEntityFactory("AdminEntity"),
				"super",
				InvalidEmailError
			);
			Add(
				new UserEntityFactory("AdminEntity"),
				"@e.c*@example.com",
				InvalidEmailError
			);
		}
	}
}