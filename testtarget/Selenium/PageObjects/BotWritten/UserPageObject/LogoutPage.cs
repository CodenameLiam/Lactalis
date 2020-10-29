

using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	///<summary>
	/// The LogoutPage Object represents the Logout page
	///</summary>
	public class LogoutPage : BasePage
	{
		public override string Url => baseUrl + "/logout";

		public LogoutPage(ContextConfiguration ContextConfiguration) : base(ContextConfiguration)
		{
		}
	}
}