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
    public class AccountSettingsPageObject
    {
        private readonly IWebDriver _webDriver;

        private static readonly By _genInfoEditButton = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]");
        private static readonly By _firstNameField = By.XPath("div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _lastNameField = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]");
        private static readonly By _companyLocationField = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-google-maps-autocomplete[1]/label[1]/input[1]");
        private static readonly By _companyLocationMatch = By.CssSelector("[class='pac-matched']");
        private static readonly By _industryField = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]");
        
        private static readonly By _emailStatus = By.XPath("//nb-paragraph[1]/div[1]/div[1]/div[2]/span[1]");
        private static readonly By _changeEmailButton = By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]");
        private static readonly By _currentPasswordForEmailField = By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _newEmailField = By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]");
        
        private static readonly By _changePasswordButton = By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]");
        private static readonly By _currentPasswordForPasswordChangeField = By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _newPasswordField = By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]");
        private static readonly By _confirmNewPasswordField = By.XPath("//div[1]/nb-account-info-password[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]");
        
        private static readonly By _cardNameField = By.XPath("//nb-stripe-card-bind[1]/div[1]/form[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _cardNumberField = By.XPath("//div[1]/form[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _cardYearField = By.XPath("//div[1]/form[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _cardCVVField = By.XPath("//div[1]/form[1]/common-input[1]/label[1]/input[1]");
       
        private static readonly By _editPhoneNumberButton = By.XPath("//div[1]/nb-account-info-phone[1]/div[1]/div[1]/nb-edit-switcher[1]/div[1]/div[1]");
        private static readonly By _currentPasswordForNumberChangeField = By.XPath("//div[1]/nb-account-info-phone[1]/div[2]/div[1]/form[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _newNumberField = By.XPath("//div[1]/form[1]/common-input-phone[1]/label[1]/input[1]");
        
        private static readonly By _saveChangesButton = By.XPath("//button[contains(text(),'Save Changes')]");
        private static readonly By _logOutButton = By.XPath("//div[contains(text(),'Log out of your account')]");
        
        private static readonly By _profileButton = By.XPath("//div[contains(text(),'PROFILE')]");
        private static readonly By _editProfileButton = By.XPath("//nb-profile-settings[1]/div[1]/div[1]/div[1]");
        private static readonly By _companyNameField = By.XPath("//div[1]/div[2]/common-input[1]/label[1]/input[1]");
        private static readonly By _companyWebsiteField = By.XPath("//div[1]/div[2]/common-input[1]/label[1]/input[1]");
        private static readonly By _descriptionField = By.XPath("//div[1]/div[1]/common-textarea[1]/label[1]/textarea[1]");
        
        private static readonly By _newFirstName = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[1]/label[1]/input[1]");
        private static readonly By _newLastName = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[2]/label[1]/input[1]");
        private static readonly By _newLocation = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-google-maps-autocomplete[1]/label[1]/input[1]");
        private static readonly By _newIndustry = By.XPath("//div[1]/nb-account-info-general-information[1]/form[1]/div[2]/div[1]/common-input[3]/label[1]/input[1]");
        private static readonly By _newEmail = By.XPath("//div[1]/nb-account-info-email-address[1]/form[1]/div[2]/div[1]/nb-paragraph[1]/div[1]/div[1]/div[1]/span[1]");
        private static readonly By _newNumber = By.XPath("//div[2]/div[1]/nb-paragraph[2]/div[1]/span[1]");

        public AccountSettingsPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public AccountSettingsPageObject GoToEditPage()
        {
            _webDriver.Navigate().GoToUrl("https://newbookmodels.com/account-settings/account-info/edit");
            return this;
        }

        public AccountSettingsPageObject ClickEditGenInfo()
        {
            _webDriver.FindElement(_genInfoEditButton).Click();
            return this;
        }
        
        public AccountSettingsPageObject ClickChangeEmail()
        {
            _webDriver.FindElement(_changeEmailButton).Click();
            return this;
        }
        
        public AccountSettingsPageObject ClickChangePassword()
        {
            _webDriver.FindElement(_changePasswordButton).Click();
            return this;
        }
        
        public AccountSettingsPageObject ClickChangeNumber()
        {
            _webDriver.FindElement(_editPhoneNumberButton).Click();
            return this;
        }
        
        public AccountSettingsPageObject ClickProfile()
        {
            _webDriver.FindElement(_profileButton).Click();
            return this;
        }
        
        public AccountSettingsPageObject ClickEditProfile()
        {
            _webDriver.FindElement(_editProfileButton).Click();
            return this;
        }
        
        public AccountSettingsPageObject ClickSave()
        {
            _webDriver.FindElement(_saveChangesButton).Click();
            return this;
        }

        public AccountSettingsPageObject ClickLogOut()
        {
            _webDriver.FindElement(_logOutButton).Click();
            return this;
        }

        public AccountSettingsPageObject SetNewFirstName(string name)
        {
            _webDriver.FindElement(_firstNameField).Clear();
            _webDriver.FindElement(_firstNameField).SendKeys(name);
            return this;
        }
        
        public AccountSettingsPageObject SetNewLastName(string name)
        {
            _webDriver.FindElement(_lastNameField).Clear();
            _webDriver.FindElement(_lastNameField).SendKeys(name);
            return this;
        }

        public AccountSettingsPageObject SetNewCompanyLocation(string location)
        {
            _webDriver.FindElement(_companyLocationField).Clear();
            _webDriver.FindElement(_companyLocationField).SendKeys(location);
            Thread.Sleep(1500);
            _webDriver.FindElement(_companyLocationMatch).Click();
            return this;
        }
        
        public AccountSettingsPageObject SetNewIndustry(string industry)
        {
            _webDriver.FindElement(_industryField).Clear();
            _webDriver.FindElement(_industryField).SendKeys(industry);
            return this;
        }

        public string GetNewFirstName()
        {
            var name = _webDriver.FindElement(_newFirstName).Text;
            return name;
        }
        
        public string GetNewLastName()
        {
            var name = _webDriver.FindElement(_newLastName).Text;
            return name;
        }
        
        public string GetNewLocation()
        {
            var name = _webDriver.FindElement(_newLocation).Text;
            return name;
        }
        
        public string GetNewIndustry()
        {
            var name = _webDriver.FindElement(_newIndustry).Text;
            return name;
        }
        
        public string GetNewEmail()
        {
            var name = _webDriver.FindElement(_newEmail).Text;
            return name;
        }
        
        public string GetEmailStatus()
        {
            var name = _webDriver.FindElement(_emailStatus).Text;
            return name;
        }
        
        public string GetNewNumber()
        {
            var name = _webDriver.FindElement(_newNumber).Text;
            return name;
        }
        
        public AccountSettingsPageObject SetNewEmail(string newEmail)
        {
            _webDriver.FindElement(_newEmailField).SendKeys(newEmail);
            return this;
        }

        public AccountSettingsPageObject SetNewPassword(string newPassword)
        {
            _webDriver.FindElement(_newPasswordField).SendKeys(newPassword);
            return this;
        }
        
        public AccountSettingsPageObject ConfirmNewPassword(string newPassword)
        {
            _webDriver.FindElement(_confirmNewPasswordField).SendKeys(newPassword);
            return this;
        }
        
        public AccountSettingsPageObject ConfirmPasswordForEmailChange(string password)
        {
            _webDriver.FindElement(_currentPasswordForEmailField).SendKeys(password);
            return this;
        }
        
        public AccountSettingsPageObject ConfirmPasswordForPasswordChange(string password)
        {
            _webDriver.FindElement(_currentPasswordForPasswordChangeField).SendKeys(password);
            return this;
        }
        
        public AccountSettingsPageObject ConfirmPasswordForNumberChange(string password)
        {
            _webDriver.FindElement(_currentPasswordForNumberChangeField).SendKeys(password);
            return this;
        }
        
        public AccountSettingsPageObject SetCardholderName()
        {
            _webDriver.FindElement(_cardNameField).SendKeys("MaBelle S Parker");
            return this;
        }
        
        public AccountSettingsPageObject SetCardNumber()
        {
            _webDriver.FindElement(_cardNumberField).SendKeys("4000 0200 0000 0000");
            return this;
        }
        
        public AccountSettingsPageObject SetExpirationDate()
        {
            _webDriver.FindElement(_cardYearField).SendKeys("0330");
            return this;
        }
        
        public AccountSettingsPageObject SetCVV()
        {
            _webDriver.FindElement(_cardCVVField).SendKeys("737");
            return this;
        }
        
        public AccountSettingsPageObject SetNewNumber()
        {
            _webDriver.FindElement(_newNumberField).SendKeys("999.111.9999");
            return this;
        }

        public AccountSettingsPageObject SetNewCompanyName(string companyName)
        {
            _webDriver.FindElement(_companyNameField).Clear();
            _webDriver.FindElement(_companyNameField).SendKeys(companyName);
            return this;
        }
        
        public AccountSettingsPageObject SetNewCompanyWebsite(string companyWebsite)
        {
            _webDriver.FindElement(_companyWebsiteField).Clear();
            _webDriver.FindElement(_companyWebsiteField).SendKeys(companyWebsite);
            return this;
        }
        
        public AccountSettingsPageObject SetDescription(string description)
        {
            _webDriver.FindElement(_descriptionField).Clear();
            _webDriver.FindElement(_descriptionField).SendKeys(description);
            return this;
        }
    }
}