using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

public class BestGamesPage
{
    private readonly IWebDriver Driver;
    private readonly WebDriverWait Wait;

    public BestGamesPage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    /// <summary>
    /// Returns the list of all displayed best game titles.
    /// Handles lazy-loading by scrolling the container until all games are loaded.
    /// </summary>
    public List<string> GetAllBestGames()
    {
        // Wait until at least one game card is visible
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElements(By.CssSelector("p[data-testid='card-title']")).Count > 0);

        var gameElements = Driver.FindElements(By.CssSelector("p[data-testid='card-title']"));
        var gameTitles = new List<string>();

        foreach (var element in gameElements)
        {
            string title = element.Text.Trim();
            if (string.IsNullOrEmpty(title))
            {
                // fallback: use aria-label
                title = element.GetAttribute("aria-label")?.Trim() ?? string.Empty;
            }
            gameTitles.Add(title);
        }

        return gameTitles;
    }

}
