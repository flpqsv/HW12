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
    public class RegistrationPage
    {
        internal IWebDriver _webDriver;
        
        private static readonly By _emailField = By.CssSelector("input[type=email]");
        private static readonly By _passwordField = By.CssSelector("input[type=password]");
        private static readonly By _confirmPasswordField = By.CssSelector("[name=password_confirm]");
        private static readonly By _firstNameField = By.CssSelector("[name=first_name]");
        private static readonly By _lastNameField = By.CssSelector("[name=last_name]");
        private static readonly By _phoneField = By.CssSelector("[name=phone_number]");
        private static readonly By _loginButton = By.CssSelector("[class^=SignInForm__submitButton]");
        private static readonly By _submitButton = By.CssSelector("[type=submit]");
        private static readonly By _finishButton = By.CssSelector("[type=submit]");
        private static readonly By _companyNameField = By.CssSelector("[name=company_name]");
        private static readonly By _companyWebsiteField = By.CssSelector("[name=company_website]");
        private static readonly By _industryNameField = By.CssSelector("[name=industry]");
        private static readonly By _locationField = By.CssSelector("[name=location]");
        private static readonly By _dropArea = By.CssSelector("[class=SignupAvatar__avatar--IxJnV]");
        
        public RegistrationPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public RegistrationPage GoToSignInPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            return this;
        }

        public RegistrationPage GoToRegistrationPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/");
            _webDriver.FindElement(By.CssSelector("[class*=Navbar__signUp]")).Click();
            return this;
        }

        public RegistrationPage SetEmail(string email)
        {
            _webDriver.FindElement(_emailField).SendKeys(email);
            return this;
        }

        public RegistrationPage SetPassword(string password)
        {
            _webDriver.FindElement(_passwordField).SendKeys(password);
            return this;
        }
        
        public RegistrationPage SetConfirmPassword(string password)
        {
            _webDriver.FindElement(_confirmPasswordField).SendKeys(password);
            return this;
        }

        public RegistrationPage SetFirstName(string firstName)
        {
            _webDriver.FindElement(_firstNameField).SendKeys(firstName);
            return this;
        }
        
        public RegistrationPage SetLastName(string lastName)
        {
            _webDriver.FindElement(_lastNameField).SendKeys(lastName);
            return this;
        }

        public RegistrationPage SetPhone(string phone)
        {
            _webDriver.FindElement(_phoneField).SendKeys(phone);
            return this;
        }

        public RegistrationPage SetCompanyName(string companyName)
        {
            _webDriver.FindElement(_companyNameField).SendKeys(companyName);
            return this;
        }
        
        public RegistrationPage SetCompanyWebsite(string companyWebsite)
        {
            _webDriver.FindElement(_companyWebsiteField).SendKeys(companyWebsite);
            return this;
        }

        public RegistrationPage ClickCompanyIndustry()
        {
            _webDriver.FindElement(_industryNameField).Click();
            _webDriver.FindElement(By.CssSelector("[class=Select__optionText--OxKln]")).Click();
            return this;
        }

        public RegistrationPage ClickLocation(string location)
        {
            _webDriver.FindElement(_locationField).SendKeys(location);
            Thread.Sleep(3000);
            _webDriver.FindElement(By.CssSelector("[class=pac-matched]")).Click();
            return this;
        }

        public RegistrationPage ClickLoginButton()
        {
            _webDriver.FindElement(_loginButton).Click();
            return this;
        }

        public RegistrationPage ClickSubmitButton()
        {
            _webDriver.FindElement(_submitButton).Click();
            return this;
        }

        public RegistrationPage ClickFinishButton()
        {
            _webDriver.FindElement(_finishButton).Click();
            return this;
        }
    }
    
    public class RegistrationTests
    {
        private IWebDriver _webDriver;
        private RegistrationPage _registrationPage;
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _registrationPage = new RegistrationPage(_webDriver);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void RegistrateNewUser()
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
        }

        private static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

        [Test]
        public void DragAndDropFile()
        {
            _registrationPage.GoToRegistrationPage();
            
            var builder = new Actions(_webDriver);

            IWebElement droparea = _registrationPage._webDriver.FindElement(By.CssSelector("[class=SignupAvatar__avatar--IxJnV]"));
            DropFile(droparea, @"/Users/Mabelle/Downloads/67913270_2607448572640247_7383304177759289344_o.jpg");
        }
        
        const string JS_DROP_FILE = "for(var b=arguments[0],k=arguments[1],l=arguments[2],c=b.ownerDocument,m=0;;){var e=b.getBoundingClientRect(),g=e.left+(k||e.width/2),h=e.top+(l||e.height/2),f=c.elementFromPoint(g,h);if(f&&b.contains(f))break;if(1<++m)throw b=Error('Element not interractable'),b.code=15,b;b.scrollIntoView({behavior:'instant',block:'center',inline:'center'})}var a=c.createElement('INPUT');a.setAttribute('type','file');a.setAttribute('style','position:fixed;z-index:2147483647;left:0;top:0;');a.onchange=function(){var b={effectAllowed:'all',dropEffect:'none',types:['Files'],files:this.files,setData:function(){},getData:function(){},clearData:function(){},setDragImage:function(){}};window.DataTransferItemList&&(b.items=Object.setPrototypeOf([Object.setPrototypeOf({kind:'file',type:this.files[0].type,file:this.files[0],getAsFile:function(){return this.file},getAsString:function(b){var a=new FileReader;a.onload=function(a){b(a.target.result)};a.readAsText(this.file)}},DataTransferItem.prototype)],DataTransferItemList.prototype));Object.setPrototypeOf(b,DataTransfer.prototype);['dragenter','dragover','drop'].forEach(function(a){var d=c.createEvent('DragEvent');d.initMouseEvent(a,!0,!0,c.defaultView,0,0,0,g,h,!1,!1,!1,!1,0,null);Object.setPrototypeOf(d,null);d.dataTransfer=b;Object.setPrototypeOf(d,DragEvent.prototype);f.dispatchEvent(d)});a.parentElement.removeChild(a)};c.documentElement.appendChild(a);a.getBoundingClientRect();return a;";

        public void DropFile(IWebElement target, string filePath, double offsetX = 0, double offsetY = 0)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            IWebDriver driver = ((RemoteWebElement)target).WrappedDriver;
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            IWebElement input = (IWebElement)jse.ExecuteScript(JS_DROP_FILE, target, offsetX, offsetY);
            input.SendKeys(filePath);
        }
    }
}
