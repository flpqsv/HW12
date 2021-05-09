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
    public class AccountSettingsTests
    {
        private IWebDriver _webDriver;
        private AccountSettingsPageObject _settingsPageObject;
        private RegistrationPageObject _registrationPageObject;
        private SignInPageObject _signInPageObject;

        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _settingsPageObject = new AccountSettingsPageObject(_webDriver);
            _registrationPageObject = new RegistrationPageObject(_webDriver);
            _signInPageObject = new SignInPageObject(_webDriver);
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

            _registrationPageObject.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(1000);

            _settingsPageObject.GoToEditPage()
                .SetNewFirstName("Svitlana")
                .SetNewLastName("Filippovych")
                .SetNewCompanyLocation("ca")
                .SetNewIndustry("Testing")
                .ClickSave();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Svitlana", _settingsPageObject.GetNewFirstName());
                Assert.AreEqual("Filippovych", _settingsPageObject.GetNewLastName());
                Assert.AreEqual("Cape Coral, FL, USA",_settingsPageObject.GetNewLocation());
                Assert.AreEqual("Testing", _settingsPageObject.GetNewIndustry());
            });
        }

        [Test]
        public void EditEmailAddress()
        {
            var email = CreateNewEmail(out var date);

            _registrationPageObject.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(1000);
            var newEmail = CreateNewEmail(out date);

            _settingsPageObject.GoToEditPage()
                .ClickChangeEmail()
                .ConfirmPasswordForEmailChange("Mabel1234!")
                .SetNewEmail($"new.{newEmail}")
                .ClickSave();
            
            Assert.AreEqual($"new.{newEmail}", _settingsPageObject.GetNewEmail());
        }

        [Test]
        public void EditPassword()
        {
            var email = CreateNewEmail(out var date);

            _registrationPageObject.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(1000);

            _settingsPageObject.GoToEditPage()
                .ConfirmPasswordForPasswordChange("Mabel1234!")
                .SetNewPassword("Svitlana1234!")
                .ConfirmNewPassword("Svitlana1234!")
                .ClickSave()
                .ClickLogOut();

            _signInPageObject.GoToSignInPage()
                .SetEmail(email)
                .SetPassword("Svitlana1234!")
                .ClickLoginButton();
            
            Thread.Sleep(2000);
            
            Assert.AreEqual("https://newbookmodels.com/join/company?goBackUrl=%2Fexplore", _webDriver.Url);
        }

        [Test]
        public void AddCard()
        {
            var email = CreateNewEmail(out var date);

            _registrationPageObject.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(1000);

            _settingsPageObject.GoToEditPage()
                .SetCardholderName()
                .SetExpirationDate()
                .SetCVV()
                .ClickSave();
        }
        
        public static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

    }
}
