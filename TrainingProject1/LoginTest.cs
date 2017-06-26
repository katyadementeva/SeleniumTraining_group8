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
        public void Login()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);
            driver.Navigate().GoToUrl("http://localhost:8081/litecart/admin");

            driver.FindElement(By.CssSelector("input[name=username]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("button[name=login]")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.notice.success")));
        }        
    }    
}
