using System;
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
            LoginToAdmin("admin", "admin");            
        }        
    }    
}
