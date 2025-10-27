using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace GamesSUT.Tests.Helpers
{
    public class WaitHelpers
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public WaitHelpers(IWebDriver driver, int timeoutInSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public IWebElement WaitForElementVisible(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement WaitForElementToBeClickable(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public bool IsElementVisible(By locator, int timeoutInSeconds = 5)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void WaitForPageLoad()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}