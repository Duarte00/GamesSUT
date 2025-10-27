using OpenQA.Selenium;
using GamesSUT.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace GamesSUT.Tests.PageObjects
{
    public class SearchComponent
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelpers _wait;

        // Locators
        private By SearchIconLocator => By.CssSelector("[data-testid='search-icon']");
        private By SearchInputLocator => By.CssSelector("input[data-testid='search-input']");
        private By ResultsContainerLocator => By.Id("results");
        private By AllResultsLocator => By.CssSelector("#results [data-testid='result-item']");
        private By CategoryResultsLocator => By.CssSelector("[data-testid='categories-results'] [data-testid='result-item']");
        private By GameResultsLocator => By.CssSelector("[data-testid='games-results'] [data-testid='result-item']");
        private By ClearButtonLocator => By.CssSelector("button[aria-label='Clear']");

        public SearchComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelpers(driver);
        }

        public void OpenSearch()
        {
            try
            {
                // If the input is already visible, no need to click the icon
                if (_wait.IsElementVisible(SearchInputLocator))
                    return;

                var searchIcon = _wait.WaitForElementToBeClickable(SearchIconLocator);

                // Scroll into view just in case it’s offscreen
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", searchIcon);

                // Use JavaScript click to bypass overlay issues
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", searchIcon);

                _wait.WaitForElementVisible(SearchInputLocator, 5);
            }
            catch (ElementClickInterceptedException)
            {
                // Fallback: Try JS click again in case of animation overlay
                var searchIcon = _driver.FindElement(SearchIconLocator);
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", searchIcon);
                _wait.WaitForElementVisible(SearchInputLocator, 5);
            }
        }
        public void EnterSearchTerm(string searchTerm)
        {
            var input = _wait.WaitForElementVisible(SearchInputLocator);
            input.Clear();
            input.SendKeys(searchTerm);
        }

        public void WaitForSearchResults()
        {
            _wait.WaitForElementVisible(ResultsContainerLocator, 10);
            _wait.WaitForElementVisible(AllResultsLocator, 10);
        }

        public List<string> GetAllResultNames()
        {
            var results = _driver.FindElements(AllResultsLocator);
            return results.Select(r => r.Text.Trim()).Where(text => !string.IsNullOrEmpty(text)).ToList();
        }

        public List<string> GetCategoryResults()
        {
            var categories = _driver.FindElements(CategoryResultsLocator);
            return categories.Select(c => c.Text.Trim()).Where(text => !string.IsNullOrEmpty(text)).ToList();
        }

        public List<string> GetGameResults()
        {
            var games = _driver.FindElements(GameResultsLocator);
            return games.Select(g => g.Text.Trim()).Where(text => !string.IsNullOrEmpty(text)).ToList();
        }

        public void ClickResultByName(string name)
        {
            var result = _driver.FindElements(AllResultsLocator)
                                .FirstOrDefault(r => r.Text.Trim().Equals(name, System.StringComparison.OrdinalIgnoreCase));
            if (result != null)
                result.Click();
            else
                throw new NoSuchElementException($"Search result '{name}' not found.");
        }

        public void ClearSearch()
        {
            var clearButton = _driver.FindElement(ClearButtonLocator);
            if (clearButton.Displayed)
                clearButton.Click();
        }
    }
}
