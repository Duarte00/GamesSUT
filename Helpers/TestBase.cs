using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;

namespace GamesSUT.Tests.Helpers
{
    public class TestBase
    {
        protected IWebDriver Driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Setup for test suite
            TestConfig.LoadConfiguration();
        }

        [SetUp]
        public void BaseSetUp()
        {
            Driver = DriverFactory.CreateDriver(TestConfig.BrowserType);
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }

            Driver?.Quit();
            Driver?.Dispose();
        }

        private void TakeScreenshot(string testName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var screenshotPath = Path.Combine(TestConfig.ScreenshotPath, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                screenshot.SaveAsFile(screenshotPath);
                TestContext.WriteLine($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }
    }

    public static class TestConfig
    {
        public static string BaseUrl { get; set; }
        public static string BrowserType { get; set; }
        public static string ScreenshotPath { get; set; }

        public static void LoadConfiguration()
        {
            // Load from appsettings.json or use defaults
            BaseUrl = Environment.GetEnvironmentVariable("SUT_BASE_URL") ?? "https://www.arkadium.com/";
            BrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE") ?? "chrome";
            ScreenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");

            if (!Directory.Exists(ScreenshotPath))
            {
                Directory.CreateDirectory(ScreenshotPath);
            }
        }
    }
}