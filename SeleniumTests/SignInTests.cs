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
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
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
            
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/");
            _webDriver.FindElement(By.CssSelector("[class*=Navbar__signUp]")).Click();

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            _webDriver.FindElement(By.CssSelector("[name=company_name]")).SendKeys("Henlo World Inc.");
            
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            _webDriver.FindElement(By.XPath("//div[contains(text(),'Log out of your account')]")).Click();
            
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            _webDriver.FindElement(By.CssSelector("input[type=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("input[type=password]")).SendKeys(password);
            _webDriver.FindElement(By.CssSelector("[class^=SignInForm__submitButton]")).Click();
            
            Actions action  = new Actions(_webDriver);
            var appIcon = _webDriver.FindElement(By.XPath("//a[contains(text(),'iPhone App')]"));
            action.MoveToElement(appIcon).Perform();
            
            Assert.That(_webDriver.Url == "https://newbookmodels.com/join/company?goBackUrl=%2Fexplore");
        }

        public static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

        [Test]
        public void SignInWithWrongEmail()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            
            _webDriver.FindElement(By.CssSelector("input[type=email]")).SendKeys("wrong_email");
            _webDriver.FindElement(By.CssSelector("input[type=password]")).SendKeys("random_password");
            _webDriver.FindElement(By.CssSelector("[class^=SignInForm__submitButton]")).Click();

            var wrongEmailMessage = _webDriver.FindElement(By.XPath("//div[contains(text(),'Invalid Email')]")).Text;
            
            Assert.AreEqual("Invalid Email", wrongEmailMessage);
        }
        
        [Test]
        public void SignInWithWrongPassword()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            
            _webDriver.FindElement(By.CssSelector("input[type=email]")).SendKeys("mabel1@gmail.com");
            _webDriver.FindElement(By.CssSelector("input[type=password]")).SendKeys("random_password");
            _webDriver.FindElement(By.CssSelector("[class^=SignInForm__submitButton]")).Click();

            var wrongPasswordMessage = _webDriver.FindElement(By.XPath("//div[contains(text(),'Please enter')]")).Text;
            
            Assert.AreEqual("Please enter a correct email and password.", wrongPasswordMessage);
        }
    }
}
