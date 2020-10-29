

using OpenQA.Selenium;
using SeleniumTests.Setup;
using System.Collections.Generic;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObject
{
	public class AllUserPage : BasePage
	{
		public List<string> ExpectedColumnHeadings = new List<string>{"Type", "Email", "Activated"};
		public override string Url => baseUrl + "/admin/user";
		public IWebElement ListHeader => FindElementExt("ListHeader");
		public IWebElement CreateNewButton => FindElementExt("CreateNewButton");
		public IWebElement SelectUserTypeModal => FindElementExt("SelectUserTypeModal");
		public IWebElement SelectUserTypeDropdown => FindElementExt("SelectUserTypeDropdown");
		public IEnumerable<IWebElement> ListHeaders => ListHeader.FindElements(By.CssSelector("th.sortable"));

		public AllUserPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("CreateNewButton", (selector: "button.icon-create", type: SelectorType.CSS));
			selectorDict.Add("ListHeader", (selector: "tr.list__header", type: SelectorType.CSS));
			selectorDict.Add("SearchBox", (selector: "search__collection", type: SelectorType.CSS));
			selectorDict.Add("SelectUserTypeModal", (selector: "div.ReactModal__Overlay", type: SelectorType.CSS));
			selectorDict.Add("SelectUserTypeDropdown", (selector: "div.ReactModal__Overlay div.input-group__dropdown", type: SelectorType.CSS));
		}
	}
}