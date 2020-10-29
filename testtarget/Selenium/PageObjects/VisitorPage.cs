

using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects
{
	///<summary>
	///The vistor page object, All pages open to visitors should extend this page
	/// Contains methods shared across all page Objects
	///</summary>
	public abstract class VisitorPage : BasePage
	{
		ed VisitorNavSection visitorNavBar;


		public VisitorPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			visitorNavBar = new VisitorNavSection(contextConfiguration);

		}


	}
}
