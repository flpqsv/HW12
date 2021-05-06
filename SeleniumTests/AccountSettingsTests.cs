using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumTests
{
    public class SettingsPage
    {
        internal IWebDriver _webDriver;
        
        private static readonly By _genInfoEditButton =
            By.XPath("nb-account-info-general-information .edit-switcher__icon_type_edit");
        private static readonly By _firstNameField = By.CssSelector("[class=input__self input__self_type_text-underline ng-untouched ng-pristine ng-valid]");

        private static readonly By _clearFirstName =
            By.CssSelector(
                "[class=ng-valid ng-touched input__self input__self_error input__self_type_text-underline ng-dirty]");
        
        public SettingsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public SettingsPage GoToEditPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            return this;
        }

        public SettingsPage ClickEditGenInfo()
        {
            _webDriver.FindElement(_genInfoEditButton).Click();
            return this;
        }

        public SettingsPage SetNewFirstName(string name)
        {
            _webDriver.FindElement(_firstNameField).Clear();
            _webDriver.FindElement(_clearFirstName).SendKeys(name);
            return this;
        }
    }
    
    public class AccountSettingsTests
    {
        private IWebDriver _webDriver;
        private SettingsPage _settingspage;
        private RegistrationPage _registrationPage;
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _settingspage = new SettingsPage(_webDriver);
            
            _registrationPage = new RegistrationPage(_webDriver);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }
        
        [Test]
        public void EditAccount()
        {
            var email = CreateNewEmail(out var date);
            
            _registrationPage.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton()
                .SetCompanyName("Henlo World Inc.")
                .SetCompanyWebsite("henloworldinc.com")
                .ClickCompanyIndustry()
                .ClickLocation("da")
                .ClickFinishButton();

            _settingspage.GoToEditPage()
                .ClickEditGenInfo()
                .SetNewFirstName("Svitlanka");
            
            _webDriver.FindElement(By.CssSelector("[class=input__self input__self_type_text-underline ng-pristine ng-valid ng-touched]")).Clear();
            _webDriver.FindElement(By.CssSelector("[class=edit-switcher__icon_type_edit]")).SendKeys("Svitlanka");
            Thread.Sleep(1000);
            _webDriver.FindElement(By.CssSelector("[class=input__self input__self_type_text-underline ng-untouched ng-pristine ng-valid pac-target-input]")).Clear();
            _webDriver.FindElement(By.CssSelector("[class=class=input__self input__self_type_text-underline ng-untouched ng-pristine ng-valid pac-target-input]")).SendKeys("ca");
            Thread.Sleep(2000);
            _webDriver.FindElement(By.CssSelector("[class=input__self input__self_type_text-underline ng-untouched ng-pristine ng-valid pac-target-input]]")).Click();
            
            
            _webDriver.FindElement(By.XPath("[@class=edit-switcher__icon_type_edit] [2]")).Click();
            
            /*var verification = _webDriver.FindElement(By.CssSelector("[class=not-verified-icon]"));
            Assert.AreEqual(verification, _webDriver.FindElement(By.CssSelector("[class=verification-status]")));*/
        }
        
        public static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

    }
}
