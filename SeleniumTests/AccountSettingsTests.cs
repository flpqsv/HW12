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
        public void CheckIfEmailIsNotVerified()
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

            _settingsPageObject.GoToEditPage();

            Assert.AreEqual("Not Verified", _settingsPageObject.GetEmailStatus());
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
        
        [Test]
        public void EditPhoneNumber()
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
                .ClickChangeNumber()
                .ConfirmPasswordForNumberChange("Mabel1234!")
                .SetNewNumber()
                .ClickSave();
            
            Thread.Sleep(1000);
            
            Assert.AreEqual("999.111.9999", _settingsPageObject.GetNewNumber());
        }
        
        [Test]
        public void LogOut()
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
                .ClickLogOut();
            
            Thread.Sleep(2000);
            
            Assert.AreEqual("https://newbookmodels.com/auth/signin", _webDriver.Url);
        }
        
        [Test]
        public void UploadPhoto()
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
                .ClickProfile()
                .ClickEditProfile();
            
            var builder = new Actions(_webDriver);

            IWebElement droparea = _webDriver.FindElement(By.XPath("//nb-profile-settings-edit[1]/form[1]/div[1]/div[1]"));
            DropFile(droparea, @"/Users/Mabelle/Downloads/67913270_2607448572640247_7383304177759289344_o.jpg");

            _settingsPageObject.ClickSave();
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
        
        public static string CreateNewEmail(out string date)
        {
            date = DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss");
            var email = $"mabel.{date}@gmail.com";
            return email;
        }
        
        [Test]
        public void AddCompanyInfo()
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
                .ClickProfile()
                .ClickEditProfile()
                .SetNewCompanyName("My Company")
                .SetNewCompanyWebsite("mycompany.com")
                .SetDescription("My description.")
                .ClickSave();
            
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("My Company", _settingsPageObject.GetCompanyName());
                Assert.AreEqual("HTTP://MYCOMPANY.COM", _settingsPageObject.GetCompanyWebsite());
                Assert.AreEqual("My description.", _settingsPageObject.GetDescription());
            });
        }
        
        [Test]
        public void TryToChangeEmailWithWrongPassword()
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
                .ConfirmPasswordForEmailChange("random_pass")
                .SetNewEmail($"new.{newEmail}")
                .ClickSave();
            
            Assert.AreEqual("Invalid old password.", _settingsPageObject.GetErrorMessage());
        }

        [Test]
        public void TryToChangePasswordWithWrongCurrentPassword()
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
                .ClickChangePassword()
                .ConfirmPasswordForPasswordChange("random_pass")
                .SetNewPassword("Mabel123!")
                .ConfirmNewPassword("Mabel123!")
                .ClickSave();
            
            Assert.AreEqual("Invalid old password.", _settingsPageObject.GetErrorMessage());
        }

    }
}
