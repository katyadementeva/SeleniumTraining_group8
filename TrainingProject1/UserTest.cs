using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TrainingProject1.app;

namespace TrainingProject1
{
    [TestFixture(Browser.Chrome, Category = "Chrome")]
    public class UserTest : OldBaseTest
    {
        public UserTest(Browser browser) : base(browser) { }

        [Test]
        public void TestUserCreation()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart");
            driver.FindElement(By.CssSelector("form[name=login_form] a")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("form[name=customer_form]")));
            Random r = new Random();
            int i = r.Next(0, 100);

            string email = string.Format("Name_{0}@gmail.com", i);
            string password = string.Format("Password_{0}", i);
            driver.FindElement(By.CssSelector("input[name=tax_id]")).SendKeys(string.Format("TaxID_{0}", i));
            driver.FindElement(By.CssSelector("input[name=company]")).SendKeys(string.Format("Company_{0}", i));
            driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys(string.Format("Name_{0}", i));
            driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys(string.Format("Surname_{0}", i));
            driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys(string.Format("Address1_{0}", i));
            driver.FindElement(By.CssSelector("input[name=address2]")).SendKeys(string.Format("address2_{0}", i));
            driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys(string.Format("12345"));
            driver.FindElement(By.CssSelector("input[name=city]")).SendKeys(string.Format("City_{0}", i));
            SelectElement countrySelectElement = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
            countrySelectElement.SelectByText("United States");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("select[name=zone_code]")));
            SelectElement stateSelectElement = new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]")));
            stateSelectElement.SelectByText("Colorado");
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=phone]")).Clear();
            driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys(string.Format("+1874745373355"));

            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-account")));
            driver.FindElement(By.CssSelector("a[href='http://localhost:8081/litecart/en/logout']")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("input[name=email]")));
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-account")));
            driver.FindElement(By.CssSelector("a[href='http://localhost:8081/litecart/en/logout']")).Click();
        }
    }
}