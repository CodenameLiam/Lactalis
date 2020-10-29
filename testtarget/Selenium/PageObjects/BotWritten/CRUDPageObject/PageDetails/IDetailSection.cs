

using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	public interface IDetailSection
	{
		List<Guid> GetAssociation(string referenceName);
		IWebElement GetHeaderTile(string attribute);
		IWebElement GetInputElement(string attribute);
		void SetInputElement(string attribute, string value);
		List<string> GetErrorMessagesForAttribute(string attribute);
		void Apply();
	}

	public interface IEntityDetailSection : IBasePage, IDetailSection { }
}