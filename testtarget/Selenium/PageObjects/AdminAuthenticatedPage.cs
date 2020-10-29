

using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects
{
	///<summary>
	///The Admin Authenticated Page represents the common elements across pages an admin would see
	///</summary>
	public class AdminAuthenticatedPage : UserAuthenticatedPage
	{
		ed AdminNavSection adminNavBar;


		public AdminAuthenticatedPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		adminNavBar = new AdminNavSection(contextConfiguration);

		}


	}
}
