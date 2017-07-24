using OpenQA.Selenium;

namespace TrainingProject1.page
{
    internal class HomePage:Page
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        internal HomePage Open()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart");
            return this;
        }

        internal void OpenProductPageByIndex(int productIndex)
        {
            driver.FindElements(By.CssSelector("div#box-most-popular li.product"))[productIndex].FindElement(By.CssSelector("a")).Click();
        }
    }
}
