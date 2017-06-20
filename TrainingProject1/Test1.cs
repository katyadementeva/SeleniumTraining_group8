using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace TrainingProject1
{
    [TestFixture]
    public class Test1
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
        public void TrainingTest1()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);
            driver.Navigate().GoToUrl("http://software-testing.ru/lms/login/index.php");
            driver.FindElement(By.CssSelector("input[id=loginbtn]")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("span.error")));
        }

        [TearDown]
        public void StopTest()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
