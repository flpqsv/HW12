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
            By.XPath("/html/body/nb-app/ng-component/nb-internal-layout/common-layout/section/div/ng-component/nb-account-info-edit/common-border/div[1]/div/nb-account-info-general-information/form/div[1]/div/nb-edit-switcher/div/div");
        private static readonly By _firstNameField = By.CssSelector("[class=input__self input__self_type_text-underline ng-untouched ng-pristine ng-valid]");
        private static readonly By _clearFirstName =
            By.CssSelector(
                "[class=ng-valid ng-touched input__self input__self_error input__self_type_text-underline ng-dirty]");
        private static readonly By _logOutButton = By.CssSelector("type=logout");
        private static readonly By _profileLink = By.XPath("/html/body/nb-app/ng-component/nb-internal-layout/common-layout/section/div/div[2]/div/div/nb-link[2]/div");
        private static readonly By _cardName = By.XPath(
            "/html/body/nb-app/ng-component/nb-internal-layout/common-layout/section/div/ng-component/nb-account-info-edit/common-border/div[4]/div/nb-stripe-card-bind-container/nb-stripe-card-bind/div/form/common-input/label/input");
        private static readonly By _cardNumber = By.XPath("//*[@id='root']/form/div/div[2]/span[1]/span[2]/div/div[2]/span/input");
        private static readonly By _cardYear = By.XPath("//*[@id='root']/form/div/div[2]/span[2]/span/span/input");
        private static readonly By _cardCVV = By.XPath("//*[@id='root']/form/div/div[2]/span[3]/span/span/input");
            
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
            Thread.Sleep(1500);
            return this;
        }

        public SettingsPage ClickLogOut()
        {
            _webDriver.FindElement(_logOutButton).Click();
            return this;
        }

        public SettingsPage SetNewFirstName(string name)
        {
            _webDriver.FindElement(_firstNameField).Clear();
            _webDriver.FindElement(_clearFirstName).SendKeys(name);
            return this;
        }

        public SettingsPage SwitchToProfileSettings()
        {
            _webDriver.FindElement(_profileLink).Click();
            return this;
        }

        public SettingsPage SetCardInfo()
        {
            _webDriver.FindElement(_cardName).SendKeys("MaBelle Parker");
            _webDriver.FindElement(_cardNumber).SendKeys("5019 5555 4444 5555");
            _webDriver.FindElement(_cardYear).SendKeys("0330");
            _webDriver.FindElement(_cardCVV).SendKeys("737");
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
                .ClickSubmitButton();

            _settingspage.GoToEditPage()
                .ClickEditGenInfo()
                .SetNewFirstName("Svitlanka");
            
        }

        [Test]
        public void EditProfile()
        {
            var email = CreateNewEmail(out var date);

            _registrationPage.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(1000);

            _settingspage.GoToEditPage()
                .SwitchToProfileSettings();
            
            Thread.Sleep(1000);
            
            Assert.That(_webDriver.Url == "https://newbookmodels.com/account-settings/profile/edit");
        }

        [Test]
        public void AddCard()
        {
            var email = CreateNewEmail(out var date);

            _registrationPage.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(1000);

            _settingspage.GoToEditPage()
                .SetCardInfo();
        }
        
        public static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

    }
}
