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
        public void CompleteRegistrateStep1()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/");
            _webDriver.FindElement(By.CssSelector("[class*=Navbar__signUp]")).Click();
            
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            _webDriver.FindElement(By.CssSelector("[name=company_name]")).SendKeys("Henlo World Inc.");
            
            Assert.AreEqual("https://newbookmodels.com/join/company", _webDriver.Url);
        }

        private static string CreateEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }

        [Test]
        public void CompleteRegistrationStep2()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/");
            _webDriver.FindElement(By.CssSelector("[class*=Navbar__signUp]")).Click();
            
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            
            _webDriver.FindElement(By.CssSelector("[name=company_name]")).SendKeys("Henlo World Inc.");
            _webDriver.FindElement(By.CssSelector("[name=company_website]")).SendKeys("henloworldinc.com");
            _webDriver.FindElement(By.CssSelector("[name=industry]")).Click();
            _webDriver.FindElement(By.CssSelector("[name=location]")).SendKeys("da");
            Thread.Sleep(1500);
            _webDriver.FindElement(By.CssSelector("[class=pac-matched]")).Click();
            _webDriver.FindElement(By.CssSelector("[class=Select__optionText--OxKln]")).Click();
            
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Finish')]")).Click();
            Actions actionProvider = new Actions(_webDriver);
            IWebElement section =
                _webDriver.FindElement(By.XPath("//div[contains(text(),'Most requested on Newbook')]"));
            actionProvider.MoveToElement(section).Build().Perform();
            
            Assert.AreEqual("https://newbookmodels.com/explore/", _webDriver.Url);
        }

        [Test]
        public void DragAndDropFile()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/");
            _webDriver.FindElement(By.CssSelector("[class*=Navbar__signUp]")).Click();
            Thread.Sleep(5000);
            var builder = new Actions(_webDriver);

            IWebElement droparea = _webDriver.FindElement(By.CssSelector("[class=SignupAvatar__avatar--IxJnV]"));
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
