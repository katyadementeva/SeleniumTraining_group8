using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TrainingProject1.page
{
    internal class CartPage : Page
    {
        [FindsBy(How = How.CssSelector, Using = "span.quantity")]
        internal IWebElement CartQuantityElement;
        [FindsBy(How = How.CssSelector, Using = "button[name=\"add_cart_product\"]")]
        internal IWebElement AddCartElement;

        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal CartPage Open()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart/en/checkout");
            return this;
        }

        internal int GetProductCount()
        {
            return driver.FindElements(By.CssSelector("li.item")).Count;
        }

        internal void RemoveProduct()
        {
            IWebElement removeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[name=\"remove_cart_item\"]")));
            IWebElement cartGrid = driver.FindElement(By.CssSelector("div[id=box-checkout-summary] table"));
            removeButton.Click();
            wait.Until(ExpectedConditions.StalenessOf(cartGrid));
        }
    }
}
