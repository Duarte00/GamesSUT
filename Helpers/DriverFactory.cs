using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;

namespace GamesSUT.Tests.Helpers
{
    public class DriverFactory
    {
        public static IWebDriver CreateDriver(string browserType = "chrome")
        {
            IWebDriver driver;

            switch (browserType.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--start-maximized");
                    chromeOptions.AddArguments("--disable-notifications");
                    chromeOptions.AddArguments("--disable-popup-blocking");
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    driver = new FirefoxDriver(firefoxOptions);
                    driver.Manage().Window.Maximize();
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    driver = new EdgeDriver(edgeOptions);
                    driver.Manage().Window.Maximize();
                    break;

                default:
                    throw new ArgumentException($"Browser type '{browserType}' is not supported");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            return driver;
        }
    }
}