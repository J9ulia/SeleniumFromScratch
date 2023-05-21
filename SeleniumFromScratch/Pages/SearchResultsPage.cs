using System;
using OpenQA.Selenium;

namespace SeleniumFromScratch.Pages
{
	public class SearchResultsPage : BasePage
    {
		public SearchResultsPage(IWebDriver driver) : base(driver) { }

        private string SearchResultsXPath = "//div[@class='search-result-main']";
        private string NoResultsFoundMessageXPath = "//p[@class='search-no-results-body']";
        private string NoResultsForPlaceFoundMessageXPath = "//span[@class='location-suggestions-header-content']";
        private string SearchPlaceXPath = "//*[@id='autocomplete-input']";
        private string SearchOutputPlaceXPath = "//div[@class='search-output-result-channel-context']";
        private string AppliedFiltersNumberXPath = "//div/div/span[contains(@class, 'filter-count')]";

        public string GetSearchPlace()
		{
			return WaitAndFind(By.XPath(SearchPlaceXPath)).GetAttribute("value");
        }

		public string GetSearchOutputPlace()
		{
			return WaitAndFind(By.XPath(SearchOutputPlaceXPath)).Text;
		}

		public string GetAppliedFiltersNumber()
		{
			return WaitAndFind(By.XPath(AppliedFiltersNumberXPath)).Text;
		}

        public bool AreSearchResultsDisplayed()
        {
            return Driver.FindElements(By.XPath(SearchResultsXPath)).Count > 0;
        }

        public bool IsNoResultsFoundMessageDisplayed()
        {
            return Driver.FindElements(By.XPath(NoResultsFoundMessageXPath)).Count > 0;
        }

        public bool IsNoResultsForPlaceFoundMessageDisplayed()
        {
            return Driver.FindElements(By.XPath(NoResultsForPlaceFoundMessageXPath)).Count > 0;
        }
    }
}

