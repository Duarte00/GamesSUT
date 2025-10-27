using OpenQA.Selenium;
using GamesSUT.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace GamesSUT.Tests.PageObjects
{
    public class SupportModal
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelpers _wait;

        // Locators
        private By ModalLocator => By.CssSelector(".modal, [role='dialog'], .support-modal");
        private By ChatWithUsLocator => By.CssSelector("[data-testid='support-chat-with-us']");
        private By FAQsLocator => By.CssSelector("[data-testid='support-faqs']");
        private By ContactSupportLocator => By.CssSelector("[data-testid='support-contact-support']");
        private By WhatsAppLocator => By.CssSelector("[data-testid='support-whatsapp']");

        private By AllSectionsLocator => By.CssSelector(".modal-section, .support-option");


        public SupportModal(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelpers(driver);
        }

        public void WaitForModalToOpen()
        {
            _wait.WaitForElementVisible(ModalLocator, 10);
        }

        public bool IsChatWithUsVisible()
        {
            return _wait.IsElementVisible(ChatWithUsLocator);
        }

        public bool IsFAQsVisible()
        {
            return _wait.IsElementVisible(FAQsLocator);
        }

        public bool IsContactSupportVisible()
        {
            return _wait.IsElementVisible(ContactSupportLocator);
        }

        public bool IsWhatsAppVisible()
        {
            return _wait.IsElementVisible(WhatsAppLocator);
        }

        public List<string> GetAllSectionTitles()
        {
            var sections = _driver.FindElements(AllSectionsLocator);
            return sections
                .Select(s => s.FindElement(By.TagName("h3")).Text.Trim())
                .ToList();
        }
    }
}