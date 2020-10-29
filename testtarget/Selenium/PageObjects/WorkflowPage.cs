

using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.BotWritten
{
	public class WorkflowPage : BasePage
	{
		public WorkflowPage(ContextConfiguration ContextConfiguration) : base(ContextConfiguration)
		{
			
		}
		
		// Gets the web element for a workflow on the page
		public IWebElement GetWorkflowElement(string workflowName) => 
			driver.FindElementsExt(By.XPath("//div[contains(@class, 'workflow__display')]"))
				.FirstOrDefault(x => x.FindElement(By.CssSelector("h4")).Text == workflowName + " Workflow");
		
		// Gets the current state that a workflow is in as a string
		public string GetCurrentStateOfWorkflow(IWebElement workflowElement) => 
			workflowElement.FindElement(By.CssSelector("div.dropdown__container > div.default")).Text
				.Replace("Current State: ", "");
		
		// Toggles the combobox for a workflow
		private void ToggleWorkflowDropdown(IWebElement workflowElement) =>
			workflowElement.FindElement(By.CssSelector("div.dropdown__container")).Click();
		
		// Returns the state options for a workflow on the page
		public List<string> GetWorkflowStateOptions(IWebElement workflowElement)
		{
			ToggleWorkflowDropdown(workflowElement);
			return workflowElement.FindElements(By.XPath("//div[contains(@class, 'visible menu transition')]/div/span"))
				.Select(x => x.Text)
				.ToList();
		}

		// Sets a workflow to a specific state
		public bool SetWorkflowState(IWebElement workflowElement, string state)
		{
			ToggleWorkflowDropdown(workflowElement);
			var stateElement = workflowElement.FindElements(By.XPath("//div[contains(@class, 'visible menu transition')]/div"))
				.FirstOrDefault(x => x.Text == state);

			if (stateElement == null)
			{
				return false;
			}
			stateElement.Click();
			return true;
		}
	}
}