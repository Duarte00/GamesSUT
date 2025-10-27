using NUnit.Framework;
using OpenQA.Selenium;
using GamesSUT.Tests.PageObjects;
using GamesSUT.Tests.Helpers;

namespace GamesSUT.Tests.Tests
{
    [TestFixture]
    public class SupportModalTests : TestBase
    {
        private HomePage _homePage;
        private SupportModal _supportModal;

        [SetUp]
        public void SetUp()
        {
            _homePage = new HomePage(Driver);
            _supportModal = new SupportModal(Driver);
        }

        [Test]
        [Category("Support")]
        [Description("Verify Support modal contains required sections")]
        public void VerifySupportModalContainsRequiredSections()
        {
            // Arrange
            _homePage.NavigateTo();

            // Act
            _homePage.ClickSupportButton();
            _supportModal.WaitForModalToOpen();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_supportModal.IsChatWithUsVisible(),
                    "Chat with us section is not visible");
                Assert.IsTrue(_supportModal.IsFAQsVisible(),
                    "FAQs section is not visible");
                Assert.IsTrue(_supportModal.IsContactSupportVisible(),
                    "Contact support section is not visible");
                Assert.IsTrue(_supportModal.IsWhatsAppVisible(),
                    "WhatsApp section is not visible");
            });

            // Log success
            TestContext.WriteLine("All required sections are present in Support modal");
        }

        //[Test]
        //[Category("Support")]
        //[Description("Verify all section texts are correct")]
        //public void VerifySupportModalSectionTexts()
        //{
        //    // Arrange
        //    _homePage.NavigateTo();

        //    // Act
        //    _homePage.ClickSupportButton();
        //    _supportModal.WaitForModalToOpen();

        //    // Assert
        //    var sections = _supportModal.GetAllSectionTitles();
        //    Assert.Multiple(() =>
        //    {
        //        Assert.IsTrue(sections.Contains("Chat with us"),
        //            "Missing 'Chat with us' text");
        //        Assert.IsTrue(sections.Contains("FAQs"),
        //            "Missing 'FAQs' text");
        //        Assert.IsTrue(sections.Contains("Contact support"),
        //            "Missing 'Contact support' text");
        //        Assert.IsTrue(sections.Contains("WhatsApp"),
        //            "Missing 'WhatsApp' text");
        //    });
        //}
    }
}