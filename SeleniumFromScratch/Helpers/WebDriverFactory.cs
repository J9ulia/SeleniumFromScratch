using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumFromScratch.Helpers
{
    public class WebDriverFactory
    {
        public static IWebDriver CreateDriver(BrowserType browserType)
        {
            IWebDriver driver;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentException("Invalid browser type specified", nameof(browserType));
            }

            return driver;
        }
    }

    public enum BrowserType
    {
        Chrome,
        Firefox
    }
}

