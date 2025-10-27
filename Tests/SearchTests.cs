using NUnit.Framework;
using OpenQA.Selenium;
using GamesSUT.Tests.PageObjects;
using GamesSUT.Tests.Helpers;
using System.Collections.Generic;

namespace GamesSUT.Tests.Tests
{
    [TestFixture]
    public class SearchTests : TestBase
    {
        private HomePage _homePage;
        private SearchComponent _searchComponent;

        [SetUp]
        public void SetUp()
        {
            _homePage = new HomePage(Driver);
            _searchComponent = new SearchComponent(Driver);
        }

        [Test]
        [Category("Search")]
        [Description("Verify search returns expected game categories and results")]
        [TestCase("Crossword")]
        [TestCase("Puzzle")]
        public void VerifySearchResultsContainExpectedGame(string searchTerm)
        {
            // Arrange
            _homePage.NavigateTo();

            // Act
            _searchComponent.OpenSearch();
            _searchComponent.EnterSearchTerm(searchTerm);
            _searchComponent.WaitForSearchResults();

            var results = _searchComponent.GetAllResultNames();

            // Assert
            Assert.IsTrue(results.Any(), "No search results found.");
            Assert.IsTrue(results.Any(r => r.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)),
                $"Search term '{searchTerm}' not found in results. Found: {string.Join(", ", results)}");

            TestContext.WriteLine($"Search for '{searchTerm}' returned: {string.Join(", ", results)}");
        }


        //[Test]
        //[Category("Search")]
        //[Description("Verify search displays results dialog")]
        //public void VerifySearchDialogDisplays()
        //{
        //    // Arrange
        //    _homePage.NavigateTo();

        //    // Act
        //    _searchComponent.OpenSearch();
        //    _searchComponent.EnterSearchTerm("Crosswords");

        //    // Assert
        //    Assert.IsTrue(_searchComponent.IsSearchDialogVisible(),
        //        "Search dialog is not visible after entering search term");
        //}
    }
}