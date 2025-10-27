using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GamesSUT.Tests
{
    [TestFixture]
    public class BestGamesTests
    {
        private IWebDriver Driver;
        private readonly List<string> ExpectedBestGames = new()
        {
            "8 Ball Pool",
            "Arkadium Bubble Shooter",
            "Arkadium Word Wipe Game",
            "Block Champ",
            "Card Sharks",
            "Crystal Collapse",
            "Crystal Collapse Summer Nights",
            "Family Feud",
            "Free Online Bridge",
            "Free Online Daily Crossword Puzzle",
            "Free Online Classic Solitaire",
            "Fruit Merge: A Suika Game",
            "Hurdle",
            "Jewel Shuffle",
            "Mahjongg Dimensions",
            "Mahjongg Solitaire",
            "Outspell Spelling Game",
            "Spider Solitaire Game",
            "The Price Is Right Plinko Pegs",
            "Who Wants to Be a Millionaire?"
        };

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.arkadium.com/free-online-games/best/");
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
        }

        [Test]
        public void VerifyAllBestGamesAreDisplayed()
        {
            ScrollToLoadAllGames();

            // Grab all game titles robustly
            var gameElements = Driver.FindElements(By.CssSelector("p[data-testid='card-title']"));
            var displayedGames = new List<string>();

            foreach (var el in gameElements)
            {
                string title = el.Text.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    title = el.GetAttribute("aria-label")?.Trim() ?? string.Empty;
                }
                if (!string.IsNullOrEmpty(title))
                    displayedGames.Add(title);
            }

            // Save a screenshot for debugging
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var screenshotPath = $"Screenshots/VerifyAllBestGamesAreDisplayed_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            Console.WriteLine($"Screenshot saved: {screenshotPath}");

            // Assert all expected games are present
            foreach (var expectedGame in ExpectedBestGames)
            {
                Assert.Contains(expectedGame, displayedGames, $"Expected game '{expectedGame}' is missing.");
            }

            // Optional: print missing/extra games for debugging
            var missing = ExpectedBestGames.Except(displayedGames).ToList();
            var extra = displayedGames.Except(ExpectedBestGames).ToList();
            if (missing.Any())
                Console.WriteLine("Missing games: " + string.Join(", ", missing));
            if (extra.Any())
                Console.WriteLine("Extra games: " + string.Join(", ", extra));
        }

        private void ScrollToLoadAllGames()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            int scrollPause = 500;
            int previousHeight = 0;

            while (true)
            {
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(scrollPause);

                int currentHeight = Convert.ToInt32(js.ExecuteScript("return document.body.scrollHeight;"));
                if (currentHeight == previousHeight)
                    break;

                previousHeight = currentHeight;
            }
        }
    }
}
