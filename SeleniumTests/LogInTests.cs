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
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _signInPageObject = new SignInPageObject(_webDriver);
            
            _registrationPageObject = new RegistrationPageObject(_webDriver);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }
        
        [Test]
        public void LogIn()
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

            Thread.Sleep(2000);
            
            _signInPageObject.GoToSignInPage()
                .SetEmail(email)
                .SetPassword(password)
                .ClickLoginButton();
            
            Thread.Sleep(2000);
            
            Assert.That(_webDriver.Url == "https://newbookmodels.com/join/company?goBackUrl=%2Fexplore");
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
        public void CheckWrongMessagePassword()
        {
            _signInPageObject.GoToSignInPage()
                .SetEmail("godedo6298@cnxingye.com")
                .SetPassword("randomPass")
                .ClickLoginButton();
            
            var actualMsg = _signInPageObject.GetUserAccountBlockMessage();
            
            Assert.AreEqual("Please enter a correct email and password.", actualMsg);
        }

        private static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

    }
}
