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
    public class RegistrationTests
    {
        private IWebDriver _webDriver;
        private RegistrationPageObject _registrationPageObject;
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _registrationPageObject = new RegistrationPageObject(_webDriver);
            _registrationPageObject.GoToRegistrationPage();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void OpenRegistrationPage()
        {
            Thread.Sleep(1500);
            Assert.AreEqual("https://newbookmodels.com/join", _webDriver.Url);
        }
        
        [Test]
        public void RegistrateUserStep1()
        {
            var email = CreateNewEmail(out var date);

            _registrationPageObject.SetFirstName("MaBelle")
                .SetLastName("Parker")
                .SetEmail(email)
                .SetPassword("Mabel1234!")
                .SetConfirmPassword("Mabel1234!")
                .SetPhone("123.321.1122")
                .ClickSubmitButton()
                .SetCompanyName("check");
            
            Assert.AreEqual("https://newbookmodels.com/join/company", _webDriver.Url);
        }
        
        [Test]
        public void RegistrateUserStep2()
        {
            var email = CreateNewEmail(out var date);

            _registrationPageObject.SetFirstName("MaBelle")
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
            
            Thread.Sleep(1500);
            
            Assert.AreEqual("https://newbookmodels.com/explore/", _webDriver.Url);
        }

        private static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

        [Test]
        public void DragAndDropFile()
        {
            var builder = new Actions(_webDriver);

            IWebElement droparea = _registrationPageObject._webDriver.FindElement(By.CssSelector("[class=SignupAvatar__avatar--IxJnV]"));
            DropFile(droparea, @"/Users/Mabelle/Downloads/67913270_2607448572640247_7383304177759289344_o.jpg");
        }
        
        const string JS_DROP_FILE = "for(var b=arguments[0],k=arguments[1],l=arguments[2],c=b.ownerDocument,m=0;;){var e=b.getBoundingClientRect(),g=e.left+(k||e.width/2),h=e.top+(l||e.height/2),f=c.elementFromPoint(g,h);if(f&&b.contains(f))break;if(1<++m)throw b=Error('Element not interractable'),b.code=15,b;b.scrollIntoView({behavior:'instant',block:'center',inline:'center'})}var a=c.createElement('INPUT');a.setAttribute('type','file');a.setAttribute('style','position:fixed;z-index:2147483647;left:0;top:0;');a.onchange=function(){var b={effectAllowed:'all',dropEffect:'none',types:['Files'],files:this.files,setData:function(){},getData:function(){},clearData:function(){},setDragImage:function(){}};window.DataTransferItemList&&(b.items=Object.setPrototypeOf([Object.setPrototypeOf({kind:'file',type:this.files[0].type,file:this.files[0],getAsFile:function(){return this.file},getAsString:function(b){var a=new FileReader;a.onload=function(a){b(a.target.result)};a.readAsText(this.file)}},DataTransferItem.prototype)],DataTransferItemList.prototype));Object.setPrototypeOf(b,DataTransfer.prototype);['dragenter','dragover','drop'].forEach(function(a){var d=c.createEvent('DragEvent');d.initMouseEvent(a,!0,!0,c.defaultView,0,0,0,g,h,!1,!1,!1,!1,0,null);Object.setPrototypeOf(d,null);d.dataTransfer=b;Object.setPrototypeOf(d,DragEvent.prototype);f.dispatchEvent(d)});a.parentElement.removeChild(a)};c.documentElement.appendChild(a);a.getBoundingClientRect();return a;";

        private void DropFile(IWebElement target, string filePath, double offsetX = 0, double offsetY = 0)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            IWebDriver driver = ((RemoteWebElement)target).WrappedDriver;
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            IWebElement input = (IWebElement)jse.ExecuteScript(JS_DROP_FILE, target, offsetX, offsetY);
            input.SendKeys(filePath);
        }
        
        [Test]
        public void CompleteStep1WithWrongInfo()
        {
            _registrationPageObject.SetFirstName("")
                .SetLastName("")
                .SetEmail("wrong_email_format")
                .SetPassword("mabelle")
                .SetConfirmPassword("123")
                .SetPhone("")
                .ClickSubmitButton();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Required", _registrationPageObject.GetWrongFirstNameMessage());
                Assert.AreEqual("Required", _registrationPageObject.GetWrongLastNameMessage());
                Assert.AreEqual("Invalid Email", _registrationPageObject.GetWrongEmailMessage());
                Assert.AreEqual("Invalid password format", _registrationPageObject.GetWrongPasswordMessage());
                Assert.AreEqual("Passwords must match", _registrationPageObject.GetWrongConfirmPasswordMessage());
                Assert.AreEqual("Invalid phone format", _registrationPageObject.GetWrongPhoneMessage());
                Assert.AreEqual("https://newbookmodels.com/join", _webDriver.Url);
            });
        }
    }
}
