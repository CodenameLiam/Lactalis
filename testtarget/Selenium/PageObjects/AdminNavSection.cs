

using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects
{
	///<summary>
	///The Admin Nav section represents the navigation bar that a admin would see
	///</summary>
	public class AdminNavSection : UserNavSection
	{

		public AdminNavSection(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		public int TotalSubmenuLinks() => AdminNavSubLink.FindElements(By.TagName("li")).Count;

		public IList<IWebElement> GetAdminNavSubmenuLinks => AdminNavSubLink.FindElements(By.TagName("li")).ToList();

		public IWebElement GetAdminNavSubmenuLink(string linkText)
		{
			return GetAdminNavSubmenuLinks.FirstOrDefault(link => linkText.Equals(link.FindElement(By.TagName("a")).Text.ToLower()));
		}

		public IList<string> GetAdminNavSubmenuValues()
		{
			return GetAdminNavSubmenuLinks.Select(link => link.FindElement(By.TagName("a")).Text.ToLower()).ToList();
		}

		// Initialise all selectors
		private void InitializeSelectors()
		{
			selectorDict.Add("AdminNavMenu", (selector: "//nav[contains(@class,'nav--collapsed')]", type: SelectorType.XPath));

			// Admin Nav Links
			selectorDict.Add("AdminNavIconHome", (selector: "//a[contains(@class,'icon-home')]", type: SelectorType.XPath));
			selectorDict.Add("AdminNavHomeLink", (selector: "//a/span[contains(text(),'Home')]", type: SelectorType.XPath));
			selectorDict.Add("AdminNavLogoutLink", (selector: "//a/span[contains(text(),'Logout')]", type: SelectorType.XPath));

			// Admin Nav Sublinks
			selectorDict.Add("AdminNavSubLink", (selector: "//ul[contains(@class,'nav__sublinks--visible')]", type: SelectorType.XPath));

			// Admin Nav Icons
			selectorDict.Add("AdminNavToggle", (selector: "//a[contains(@class,'expand-icon')]", type: SelectorType.XPath));
			selectorDict.Add("AdminNavIconUsers", (selector: "//a[contains(@class,'icon-person-group')]", type: SelectorType.XPath));
			selectorDict.Add("AdminNavIconEntities", (selector: "//a[contains(@class,'icon-list')]", type: SelectorType.XPath));
			selectorDict.Add("AdminNavIconLogout", (selector: "//a[contains(@class,'icon-logout')]", type: SelectorType.XPath));
		}

		// Admin Nav Menu section
		public IWebElement AdminNavMenu => FindElementExt("AdminNavMenu");

		// Admin Nav Links
		public IWebElement AdminNavHomeLink => FindElementExt("AdminNavHomeLink");
		public IWebElement AdminNavUsersLink => FindElementExt("AdminNavUsersLink");
		public IWebElement AdminNavLogoutLink => FindElementExt("AdminNavLogoutLink");
		public IWebElement AdminNavEntitiesLink => FindElementExt("AdminNavEntitiesLink");

		// Admin Nav Sublinks
		public IWebElement AdminNavSubLink => FindElementExt("AdminNavSubLink");
		public IWebElement AdminNavToggle => FindElementExt("AdminNavToggle");

		// Admin Nav link Icons
		public IWebElement AdminNavIconHome => FindElementExt("AdminNavIconHome");
		public IWebElement AdminNavIconUsers => FindElementExt("AdminNavIconUsers");
		public IWebElement AdminNavIconEntities => FindElementExt("AdminNavIconEntities");
		public IWebElement AdminNavIconLogout => FindElementExt("AdminNavIconLogout");

	}
}
