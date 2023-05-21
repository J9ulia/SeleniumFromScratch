using System;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumFromScratch.Helpers;
using SeleniumFromScratch.Pages;

namespace SeleniumFromScratch.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]

    public class KoopTests : BaseTest
    {
        public KoopTests(BrowserType browserType) : base(browserType) { }

        [SetUp]
        public void SetUp()
        {
            FundaHomePage homePage = new FundaHomePage(driver);

            homePage.AcceptCookies();
            Assert.IsTrue(homePage.CheckHomePageUrl());
        }

        [Test]
        public void ValidSearchTest()
        {
            FundaHomePage homePage = new FundaHomePage(driver);

            SearchResultsPage searchResultsPage = homePage.QuickSearch("Amsterdam", "5", false, "100000", false, "500000");

            Assert.AreEqual("Gemeente Amsterdam", searchResultsPage.GetSearchPlace());
            Assert.AreEqual("in Amsterdam", searchResultsPage.GetSearchOutputPlace());
            Assert.AreEqual("1", searchResultsPage.GetAppliedFiltersNumber());
            Assert.IsTrue(searchResultsPage.AreSearchResultsDisplayed());
        }

        [Test]
        public void EmptySearchTest()
		{
            FundaHomePage homePage = new FundaHomePage(driver);

            SearchResultsPage searchResultsPage = homePage.QuickSearch("", "", false, "", false, "");

            Assert.AreEqual("Nederland", searchResultsPage.GetSearchPlace());
            Assert.AreEqual("in Nederland", searchResultsPage.GetSearchOutputPlace());
            Assert.AreEqual("0", searchResultsPage.GetAppliedFiltersNumber());
            Assert.IsTrue(searchResultsPage.AreSearchResultsDisplayed());
        }

        [Test]
        public void InvalidPriceRangeTest()
        {
            FundaHomePage homePage = new FundaHomePage(driver);
            SearchResultsPage searchResultsPage = new SearchResultsPage(driver);

            homePage.FillInSearchForm("Rotterdam", "10", true, "10000", true, "5");

            Thread.Sleep(2000);
            Assert.IsTrue(homePage.IsMaximumPriceFieldHighlighted());
            homePage.ClickSearch();

            Assert.AreEqual("Gemeente Rotterdam", searchResultsPage.GetSearchPlace());
            Assert.AreEqual("in Rotterdam", searchResultsPage.GetSearchOutputPlace());
            Assert.IsFalse(searchResultsPage.AreSearchResultsDisplayed());
            Assert.IsTrue(searchResultsPage.IsNoResultsFoundMessageDisplayed());
        }

        [Test]
        public void InvalidLocationTest()
        {
            FundaHomePage homePage = new FundaHomePage(driver);
            SearchResultsPage searchResultsPage = new SearchResultsPage(driver);

            homePage.FillInSearchForm("InvalidCity", "5", false, "100000", false, "500000");
            homePage.ClickSearch();
            Assert.IsTrue(homePage.IsNoResultsTextShown());
            homePage.ClickSearch();

            Assert.IsFalse(searchResultsPage.AreSearchResultsDisplayed());
            Assert.IsTrue(searchResultsPage.IsNoResultsForPlaceFoundMessageDisplayed());
        }

        [Test]
        public void NoSearchResultsTest()
        {
            FundaHomePage homePage = new FundaHomePage(driver);

            SearchResultsPage searchResultsPage = homePage.QuickSearch("Diemen", "1", true, "100000000", false, "");

            Assert.AreEqual("Gemeente Diemen", searchResultsPage.GetSearchPlace());
            Assert.AreEqual("in Diemen", searchResultsPage.GetSearchOutputPlace());
            Assert.AreEqual("1", searchResultsPage.GetAppliedFiltersNumber());
            Assert.IsFalse(searchResultsPage.AreSearchResultsDisplayed());
            Assert.IsTrue(searchResultsPage.IsNoResultsFoundMessageDisplayed());
        }
    }
}

