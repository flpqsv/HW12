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
        
        [SetUp]
        public void Test()
        {
            _webDriver = new ChromeDriver("/Users/MaBelle/Downloads/");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/");
            _webDriver.FindElement(By.CssSelector("[class*=Navbar__signUp]")).Click();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void EditGenInformation()
        {
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
            _webDriver.FindElement(By.CssSelector("[class=Select__optionText--OxKln]")).Click();
            Thread.Sleep(1000);
            _webDriver.FindElement(By.CssSelector("[name=location]")).SendKeys("da");
            Thread.Sleep(2000);
            _webDriver.FindElement(By.CssSelector("[class=pac-matched]")).Click();
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Finish')]")).Click();

            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]")).Click();
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).Clear();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).SendKeys("Svitlana");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).Clear();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).SendKeys("Filippovych");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-google-maps-autocomplete[1]/label[1]/input[1]")).Clear();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-google-maps-autocomplete[1]/label[1]/input[1]")).SendKeys("ca");
            Thread.Sleep(2000);
            _webDriver.FindElement(By.CssSelector("[class='pac-matched']")).Click();
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]")).Clear();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]")).SendKeys("Testing");
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();

            var firstNameField = _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).GetAttribute("value");
            var lastNameField = _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).GetAttribute("value");
            var companyLocationField = _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-google-maps-autocomplete[1]/label[1]/input[1]")).GetAttribute("value");
            var industryField = _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]")).GetAttribute("value");
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Svitlana", firstNameField);
                Assert.AreEqual("Filippovych", lastNameField);
                Assert.AreEqual("Cape Coral, FL, USA",companyLocationField);
                Assert.AreEqual("Testing", industryField);
            });
        }

        [Test]
        public void EditEmailAddress()
        {
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
            _webDriver.FindElement(By.CssSelector("[class=Select__optionText--OxKln]")).Click();
            Thread.Sleep(1000);
            _webDriver.FindElement(By.CssSelector("[name=location]")).SendKeys("da");
            Thread.Sleep(2000);
            _webDriver.FindElement(By.CssSelector("[class=pac-matched]")).Click();
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Finish')]")).Click();

            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]")).Click();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).SendKeys("Mabel1234!");
            
            var newEmail = CreateEmail(out date);
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).SendKeys($"new.{newEmail}");
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();

            var currentEmail = _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/nb-paragraph[1]/div[1]/div[1]/div[1]/span[1]")).Text;
            
            Assert.AreEqual($"new.{newEmail}", currentEmail);
        }

        [Test]
        public void CheckIfEmailIsNotVerified()
        {
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
            _webDriver.FindElement(By.CssSelector("[class=Select__optionText--OxKln]")).Click();
            Thread.Sleep(1000);
            _webDriver.FindElement(By.CssSelector("[name=location]")).SendKeys("da");
            Thread.Sleep(2000);
            _webDriver.FindElement(By.CssSelector("[class=pac-matched]")).Click();
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Finish')]")).Click();

            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");

            var emailStatus = _webDriver.FindElement(By.XPath("//nb-paragraph[1]/div[1]/div[1]/div[2]/span[1]")).Text;

            Assert.AreEqual("Not Verified", emailStatus);
        }

        [Test]
        public void EditPassword()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]")).Click();

            var newPassword = "MaBelle123!";
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).SendKeys(newPassword);
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]")).SendKeys(newPassword);
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.XPath("//div[contains(text(),'Log out of your account')]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            Thread.Sleep(2000);
            _webDriver.FindElement(By.CssSelector("input[type=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("input[type=password]")).SendKeys(newPassword);
            _webDriver.FindElement(By.CssSelector("[class^=SignInForm__submitButton]")).Click();
            Thread.Sleep(2000);
            
            Assert.AreEqual("https://newbookmodels.com/join/company?goBackUrl=%2Fexplore", _webDriver.Url);
        }

        [Test]
        public void AddCard()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//nb-stripe-card-bind[1]/div[1]/form[1]/common-input[1]/label[1]/input[1]")).SendKeys("MaBelle S Parker");
            _webDriver.FindElement(By.XPath("//div[1]/form[1]/common-input[1]/label[1]/input[1]")).SendKeys("4000 0200 0000 0000");
            _webDriver.FindElement(By.XPath("//div[1]/form[1]/common-input[1]/label[1]/input[1]")).SendKeys("0330");
            _webDriver.FindElement(By.XPath("//div[1]/form[1]/common-input[1]/label[1]/input[1]")).SendKeys("737");
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save')]")).Click();
        }

        [Test]
        public void EditPhoneNumber()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-phone[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]")).Click();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-phone[1]/div[2]/div[1]/form[1]/common-input[1]/label[1]/input[1]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.XPath("//div[1]/form[1]/common-input-phone[1]/label[1]/input[1]")).SendKeys("999.111.9999");
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();
            Thread.Sleep(2000);

            var updatedNumber = _webDriver.FindElement(By.XPath("//div[2]/div[1]/nb-paragraph[2]/div[1]/span[1]")).Text;
            
            Assert.AreEqual("999.111.9999", updatedNumber);
        }

        [Test]
        public void LogOut()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[contains(text(),'Log out of your account')]")).Click();
            Thread.Sleep(2000);
            
            Assert.AreEqual("https://newbookmodels.com/auth/signin", _webDriver.Url);
        }

        [Test]
        public void UploadPhoto()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[contains(text(),'PROFILE')]")).Click();
            _webDriver.FindElement(By.XPath("//nb-profile-settings[1]/div[1]/div[1]/div[1]")).Click();
            
            var builder = new Actions(_webDriver);

            IWebElement droparea = _webDriver.FindElement(By.XPath("//nb-profile-settings-edit[1]/form[1]/div[1]/div[1]"));
            DropFile(droparea, @"/Users/Mabelle/Downloads/67913270_2607448572640247_7383304177759289344_o.jpg");
            
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();
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

        [Test]
        public void AddCompanyInfo()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(2000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[contains(text(),'PROFILE')]")).Click();
            _webDriver.FindElement(By.XPath("//nb-profile-settings[1]/div[1]/div[1]/div[1]")).Click();
            
            _webDriver.FindElement(By.XPath("//div[1]/div[2]/common-input[1]/label[1]/input[1]")).SendKeys("My Company");
            _webDriver.FindElement(By.XPath("//div[2]/div[2]/common-input[1]/label[1]/input[1]")).SendKeys("mycompany.com");
            _webDriver.FindElement(By.XPath("//div[1]/div[1]/common-textarea[1]/label[1]/textarea[1]")).SendKeys("My description.");
            
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();
            var companyName = _webDriver
                .FindElement(By.XPath("//nb-profile-settings-view[1]/div[1]/div[2]/div[1]/div[1]")).Text;
            var companyWebsite = _webDriver.FindElement(By.XPath("//a[@target='_blank']")).Text;
            var description = _webDriver.FindElement(By.XPath("//nb-profile-settings-view[1]/div[1]/div[3]")).Text;
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("My Company", companyName);
                Assert.AreEqual("HTTP://MYCOMPANY.COM", companyWebsite);
                Assert.AreEqual("My description.", description);
            });
        }
        
        [Test]
        public void TryToChangeEmailWithWrongPassword()
        {
            var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(1000);

            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]")).Click();
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).SendKeys("random_pass");
            
            var newEmail = CreateEmail(out date);
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).SendKeys($"new.{newEmail}");
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();

            var systemResponse = _webDriver.FindElement(By.XPath("//span[contains(text(),'Invalid old password.')]")).Text;
            
            Assert.AreEqual("Invalid old password.", systemResponse);
        }

        [Test]
        public void TryToChangePasswordWithWrongCurrentPassword()
        {
             var email = CreateEmail(out var date);

            _webDriver.FindElement(By.CssSelector("[name=first_name]")).SendKeys("MaBelle");
            _webDriver.FindElement(By.CssSelector("[name=last_name]")).SendKeys("Parker");
            _webDriver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            _webDriver.FindElement(By.CssSelector("[name=password]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=password_confirm]")).SendKeys("Mabel1234!");
            _webDriver.FindElement(By.CssSelector("[name=phone_number]")).SendKeys("123.321.1122");
            _webDriver.FindElement(By.CssSelector("[type=submit]")).Click();
            Thread.Sleep(1000);
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]")).Click();

            var newPassword = "MaBelle123!";
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]")).SendKeys("random_pass");
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]")).SendKeys(newPassword);
            _webDriver.FindElement(By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]")).SendKeys(newPassword);
            _webDriver.FindElement(By.XPath("//button[contains(text(),'Save Changes')]")).Click();
            
            var systemResponse = _webDriver.FindElement(By.XPath("//span[contains(text(),'Invalid old password.')]")).Text;
            
            Assert.AreEqual("Invalid old password.", systemResponse);
        }
        
        private static string CreateEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            var email = $"mabel1.{date}@gmail.com";
            return email;
        }
    }
}
