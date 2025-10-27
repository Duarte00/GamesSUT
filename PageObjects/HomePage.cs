using OpenQA.Selenium;
using GamesSUT.Tests.Helpers;
using NUnit.Framework;

namespace GamesSUT.Tests.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelpers _wait;

        // Locators
        private By SupportButtonLocator => By.CssSelector("button[data-testid='topbar-support-icon']");
        private By BestSectionLocator => By.CssSelector("a[href*='best'], .best-section-link, nav a:contains('Best')");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelpers(driver);
        }

        public void AcceptCookiesIfPresent()
        {
            try
            {
                var acceptButton = _wait.WaitForElementToBeClickable(
                    By.CssSelector("button#accept-btn, button[aria-label='Agree']"), 5);

                if (acceptButton != null)
                {
                    acceptButton.Click();
                    TestContext.WriteLine("Agree");
                }
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("No cookie banner appeared — continuing.");
            }
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(TestConfig.BaseUrl);
            _wait.WaitForPageLoad();
            AcceptCookiesIfPresent();
        }

        public void ClickSupportButton()
        {
            var supportButton = _wait.WaitForElementToBeClickable(SupportButtonLocator);
            supportButton.Click();
        }

        public void NavigateToBestSection()
        {
            var bestSectionLink = _wait.WaitForElementToBeClickable(BestSectionLocator);
            bestSectionLink.Click();
        }
    }
}