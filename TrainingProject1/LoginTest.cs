﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TrainingProject1
{
    [TestFixture(Browser.Chrome, Category = "Chrome")]
    [TestFixture(Browser.IE, Category = "IE")]
    [TestFixture(Browser.FirefoxNew, Category ="FirefoxNew")]
    [TestFixture(Browser.FirefoxOld, Category = "FirefoxOld")]
    [TestFixture(Browser.FirefoxNightly, Category = "FirefoNightly")]
    public class LoginTest:BaseTest
    {
        public LoginTest(Browser browser) : base(browser) { }

        [Test]
        public void TestLogin()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart/admin");
            Login("admin", "admin");
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.notice.success")));
        }        
    }    
}
