

using SeleniumTests.PageObjects.BotWritten.UserPageObjects;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects
{
	///<summary>
	/// The User Authenticated Page represents the common elements across pages a logged in user would see
	///</summary>
	public class UserAuthenticatedPage : VisitorPage
	{
		ed UserNavSection userNavBar;


		public UserAuthenticatedPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			userNavBar = new UserNavSection(contextConfiguration);

		}


		public LoginPage Logout()
		{
			driver.GoToUrlExt(baseUrl + "logout");
			return new LoginPage(contextConfiguration);
		}
	}
}