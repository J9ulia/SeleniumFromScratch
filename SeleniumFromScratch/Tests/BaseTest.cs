using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumFromScratch.Helpers;

namespace SeleniumFromScratch.Tests
{
	public class BaseTest
	{
		public IWebDriver driver;
		private BrowserType browserType;

        public BaseTest(BrowserType browserType)
		{
			this.browserType = browserType;
			Console.WriteLine($"instance for {browserType} created");
		}

        [OneTimeSetUp]
        public void Open()
		{
            Console.WriteLine($"open {browserType}");
            driver = WebDriverFactory.CreateDriver(browserType);
            driver.Manage().Window.Maximize();
            // driver.Navigate().GoToUrl("");
            driver.Url = "https://www.funda.nl/en/";
        }

        [OneTimeTearDown]
		public void Close()
		{
			driver.Quit();
		}
	}
}