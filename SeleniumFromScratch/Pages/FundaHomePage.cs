using System;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFromScratch.Pages
{
	public class FundaHomePage : BasePage
	{
		public FundaHomePage(IWebDriver driver) : base(driver) { }

        private string HomePageUrl = "https://www.funda.nl/en/";
        private string AcceptCookiesXPath = "//*[@id='onetrust-accept-btn-handler']";
        private string SearchButtonXPath = "//button[text()='Search']";
        private string InvalidMaximumPriceFieldXPath = "//div[@class='range-filter-selector is-invalid']";
        private string NoResultsTextXPath = "//p[text()='No results, try something else...']";
        private string MinPriceFieldXPath = "//*[@id='range-filter-selector-select-filter_koopprijsvan']";
        private string MinPriceOtherOptionXPath = "//*[@id='range-filter-selector-select-filter_koopprijsvan']/option[1]";
        private string MinPriceInputXPath = "//input[@name='filter_KoopprijsVan']";
        private string MaxPriceFieldXPath = "//*[@id='range-filter-selector-select-filter_koopprijstot']";
        private string MaxPriceOtherOptionXPath = "//*[@id='range-filter-selector-select-filter_koopprijstot']/option[1]";
        private string MaxPriceInputXPath = "//input[@name='filter_KoopprijsTot']";
        private string BannerXPath = "//div[@class='search-banner']";

        public void AcceptCookies()
        {
            ClickOnExistingElement(By.XPath(AcceptCookiesXPath));
        }

        public bool CheckHomePageUrl()
		{
            return (CurrentURL() == HomePageUrl);
        }

        public void ClickSearch()
        {
            ClickOnClickableElement(By.XPath(SearchButtonXPath));
        }

        private void SelectPlace(string value)
        {
            WaitAndFind(By.Id("autocomplete-input")).SendKeys(value);
        }

        private void SelectRange(string value)
        {
            SelectElementFromDropDown(By.Name("filter_Straal"), value);
        }

        private void SelectFrom(string value)
        {
            SelectElementFromDropDown(By.Name("filter_KoopprijsVan"), value);
        }

        private void SelectTo(string value)
        {
            SelectElementFromDropDown(By.Name("filter_KoopprijsTot"), value);
        }

        public SearchResultsPage QuickSearch(string location, string range, bool otherMinPrice, string minPrice, bool otherMaxPrice, string maxPrice)
        {
            FillInSearchForm(location, range, otherMinPrice, minPrice, otherMaxPrice, maxPrice);
            ClickSearch();
            return new SearchResultsPage(Driver);
        }

        public void FillInSearchForm(string location, string range, bool otherMinPrice, string minPrice, bool otherMaxPrice, string maxPrice)
        {
            SelectPlace(location);
            if (range.Length > 0)
            {
                SelectRange(range);
            }

            if ((minPrice.Length > 0) && !otherMinPrice)
            {
                SelectFrom(minPrice);
            }

            if (otherMinPrice)
            {
                WaitAndFind(By.XPath(MinPriceFieldXPath)).Click();
                WaitAndFind(By.XPath(MinPriceOtherOptionXPath)).Click();
                // SelectFrom("other");
                if (minPrice.Length > 0)
                {
                    WaitAndFind(By.XPath(MinPriceInputXPath)).SendKeys(minPrice);
                    WaitAndFind(By.XPath(BannerXPath)).Click();
                }
            }

            if ((maxPrice.Length > 0) && !otherMaxPrice)
            {
                SelectTo(maxPrice);
            }

            if (otherMaxPrice)
            {
                WaitAndFind(By.XPath(MaxPriceFieldXPath)).Click();
                WaitAndFind(By.XPath(MaxPriceOtherOptionXPath)).Click();
                // SelectFrom("other"); // does not work
                if (minPrice.Length > 0)
                {
                    WaitAndFind(By.XPath(MaxPriceInputXPath)).SendKeys(maxPrice);
                    WaitAndFind(By.XPath(BannerXPath)).Click();
                }
            }
        }

        public bool IsMaximumPriceFieldHighlighted()
        {
            return Driver.FindElements(By.XPath(InvalidMaximumPriceFieldXPath)).Count > 0;
        }

        public bool IsNoResultsTextShown()
        {
            return Driver.FindElements(By.XPath(NoResultsTextXPath)).Count > 0;
        }
    }
}

