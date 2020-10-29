

using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.TopbarAdminPageObject
{
	/// <summary>
	/// POM for top bar menu component
	/// </summary>
	public class TopBarMenuAdmin : BasePage
	{
		public TopBarMenuAdmin(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		// Initialise all selectors
		private void InitializeSelectors()
		{
			selectorDict.Add("TopBarLink", 	 (selector: "//*[@class='admin__top-bar']//a", type: SelectorType.XPath));
		}

		public IWebElement TopBarLink => FindElementExt("TopBarLink");
	}
}