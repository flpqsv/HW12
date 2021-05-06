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
    public class SignInPage
    {
        internal IWebDriver _webDriver;
        
        private static readonly By _emailField = By.CssSelector("input[type=email]");
        private static readonly By _passwordField = By.CssSelector("input[type=password]");
        private static readonly By _loginButton = By.CssSelector("[class^=SignInForm__submitButton]");
        private static readonly By _errorMsg = By.XPath("//*[contains(@class, 'SignInForm__submitButton')]/../../div[contains(@class, 'PageForm')][last()]");
        
        public SignInPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public SignInPage GoToSignInPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            return this;
        }
        
        public SignInPage SetEmail(string email)
        {
            _webDriver.FindElement(_emailField).SendKeys(email);
            return this;
        }

        public SignInPage SetPassword(string password)
        {
            _webDriver.FindElement(_passwordField).SendKeys(password);
            return this;
        }
        
        public SignInPage ClickLoginButton()
        {
            _webDriver.FindElement(_loginButton).Click();
            return this;
        }

        public string GetUserAccountBlockMessage()
        {
            var errorMsg = _webDriver.FindElement(_errorMsg).Text;
            return errorMsg;
        }
    }
    
    public class LogInPageTests
    {
        private IWebDriver _webDriver;
        private SignInPage _signInPage;
        private RegistrationPage _registrationPage;
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _signInPage = new SignInPage(_webDriver);
            
            _registrationPage = new RegistrationPage(_webDriver);
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

            _registrationPage.GoToRegistrationPage()
                .SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword(password)
                .SetConfirmPassword(password)
                .SetPhone("123.321.1122")
                .ClickSubmitButton();

            Thread.Sleep(2000);
            
            _signInPage.GoToSignInPage()
                .SetEmail(email)
                .SetPassword(password)
                .ClickLoginButton();
            
            Thread.Sleep(2000);
            
            Assert.That(_webDriver.Url == "https://newbookmodels.com/join/company?goBackUrl=%2Fexplore");
        }

        [Test]
        public void CheckUserAccountBlockMessage()
        {
            _signInPage.GoToSignInPage()
                .SetEmail("godedo6298@cnxingye.com")
                .SetPassword("Mabel123!")
                .ClickLoginButton();
            
            Thread.Sleep(1000);
            
            var actualMsg = _signInPage.GetUserAccountBlockMessage();
            
            Assert.AreEqual("User account is blocked.", actualMsg);
        }

        [Test]
        public void CheckWrongMessagePassword()
        {
            _signInPage.GoToSignInPage()
                .SetEmail("godedo6298@cnxingye.com")
                .SetPassword("randomPass")
                .ClickLoginButton();
            
            var actualMsg = _signInPage.GetUserAccountBlockMessage();
            
            Assert.AreEqual("Please enter a correct email and password.", actualMsg);
        }
        
        public static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

    }
}
