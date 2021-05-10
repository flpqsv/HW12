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
    public class SignInTests
    {
        private IWebDriver _webDriver;
        private SignInPageObject _signInPageObject;
        private RegistrationPageObject _registrationPageObject;
        private AccountSettingsPageObject _accountSettingsPageObject;
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _signInPageObject = new SignInPageObject(_webDriver);
            _registrationPageObject = new RegistrationPageObject(_webDriver);
            _accountSettingsPageObject = new AccountSettingsPageObject(_webDriver);
            
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }
        
        [Test]
        public void SignInSuccessfully()
        {
            var email = CreateNewEmail(out var date);
            var password = "Mabel1234!";

            _registrationPageObject.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword(password)
                .SetConfirmPassword(password)
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            _accountSettingsPageObject.GoToEditPage()
                .ClickLogOut();

            Thread.Sleep(1000);
            
            _signInPageObject.SetEmail(email)
                .SetPassword(password)
                .ClickLoginButton();
            
            Thread.Sleep(1000);
            
            Assert.AreEqual("https://newbookmodels.com/join/company?goBackUrl=%2Fexplore", _webDriver.Url);
        }

        [Test]
        public void CheckUserAccountBlockMessage()
        {
            _signInPageObject.GoToSignInPage()
                .SetEmail("godedo6298@cnxingye.com")
                .SetPassword("Mabel123!")
                .ClickLoginButton();
            
            Thread.Sleep(1000);
            
            var actualMsg = _signInPageObject.GetUserAccountBlockMessage();
            
            Assert.AreEqual("User account is blocked.", actualMsg);
        }

        [Test]
        public void SignInWithWrongPassword()
        {
            _signInPageObject.GoToSignInPage()
                .SetEmail("godedo6298@cnxingye.com")
                .SetPassword("randomPass")
                .ClickLoginButton();
            
            Assert.AreEqual("Please enter a correct email and password.", _signInPageObject.GetWrongPasswordMessage());
        }
        
        [Test]
        public void SignInWithWrongEmail()
        {
            _signInPageObject.GoToSignInPage()
                .SetEmail("wrong_email")
                .SetPassword("Mabel1234!")
                .ClickLoginButton();
            
            Assert.AreEqual("Invalid Email", _signInPageObject.GetWrongEmailMessage());
        }

        private static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

    }
}
