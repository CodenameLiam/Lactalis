

namespace SeleniumTests.Setup
{
	public class SeleniumSettings
	{
		public string Webdriver { get; set; } = "chrome";
		public string Locale { get; set; } = "en-AU";
		public int Timeout { get; set; } = 5000;
		public int PollInterval { get; set; } = 100;
		public bool Headless { get; set; } = true;
		public string EdgeChromiumPath { get; set; }
		public bool OverwriteDefault { get; set; } = false;
		public int Width { get; set; } = 1920;
		public int Height { get; set; } = 1080;
		public bool FastText { get; set; } = false;
		public bool EnableScreenshots { get; set; } = true;
		public bool EnablePostTestScreenshot { get; set; } = true;
	}
}