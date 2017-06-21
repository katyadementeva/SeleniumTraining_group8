using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TrainingProject1
{
    [TestFixture]
    public class LoginTest
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartTest()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

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

        [TearDown]
        public void StopTest()
        {
            driver.Quit();
            driver.Dispose();
        }
    }    
}
