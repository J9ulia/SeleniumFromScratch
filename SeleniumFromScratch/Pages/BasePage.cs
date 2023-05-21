using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumFromScratch.Pages
{
	public class BasePage
	{
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }

        protected void ClickOnClickableElement(By locator) => Wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();

        protected void ClickOnExistingElement(By locator) => Wait.Until(ExpectedConditions.ElementExists(locator)).Click();

        protected IWebElement WaitAndFind(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return Driver.FindElement(locator);
        }

        protected string CurrentURL() => Driver.Url.ToString();

        protected void SelectElementFromDropDown(By locator, string value)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            SelectElement SelectForm = new SelectElement(Driver.FindElement(locator));
            SelectForm.SelectByValue(value);
        }

        // could be usefull
        /*
        protected IWebElement LocateElement(By locator) => Driver.FindElement(locator);
        protected void SetText(By locator, string text) => LocateElement(locator).SendKeys(text);
        protected string GetTextOfElement(By locator) => LocateElement(locator).Text;
        protected IWebElement GetElementAfterWaiting(By locator) => Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        protected bool IsElementDisplayedImmediately(By locator) => LocateElement(locator).Displayed;
        protected bool IsElementDisplayedAfterWaiting(By locator) => Wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
        protected void Click(By locator) => Driver.FindElement(locator).Click();
        protected void ClickOnElementAfterWaiting(By locator) => Wait.Until(ExpectedConditions.ElementIsVisible(locator)).Click();
        */
    }
}

