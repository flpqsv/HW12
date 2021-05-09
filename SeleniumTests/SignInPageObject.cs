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
    public class SignInPageObject
    {
        private readonly IWebDriver _webDriver;

        private static readonly By _emailField = By.CssSelector("input[type=email]");
        private static readonly By _passwordField = By.CssSelector("input[type=password]");
        private static readonly By _submitButton = By.CssSelector("[class^=SignInForm__submitButton]");
        private static readonly By _wrongEmailErrorMessage = By.XPath("//div[contains(text(),'Invalid Email')]");
        private static readonly By _wrongPasswordErrorMessage = By.XPath("//div[contains(text(),'Please enter')]");
        private static readonly By _accountBlockedMessage = By.XPath("//div[contains(text(),'User account is blocked.')]");
        
        public SignInPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public SignInPageObject GoToSignInPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            return this;
        }

        public SignInPageObject SetEmail(string email)
        {
            _webDriver.FindElement(_emailField).SendKeys(email);
            return this;
        }

        public SignInPageObject SetPassword(string password)
        {
            _webDriver.FindElement(_passwordField).SendKeys(password);
            return this;
        }

        public SignInPageObject ClickLoginButton()
        {
            _webDriver.FindElement(_submitButton).Click();
            return this;
        }

        public string GetWrongEmailMessage()
        {
            var message = _webDriver.FindElement(_wrongEmailErrorMessage).Text;
            return message;
        }
        
        public string GetWrongPasswordMessage()
        {
            var message = _webDriver.FindElement(_wrongPasswordErrorMessage).Text;
            return message;
        }
        
        public string GetUserAccountBlockMessage()
        {
            var message = _webDriver.FindElement(_accountBlockedMessage).Text;
            return message;
        }
    }
}