using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TrainingProject1.page
{
    internal class ProductPage : Page
    {
        [FindsBy(How = How.CssSelector, Using = "span.quantity")]
        internal IWebElement CartQuantityElement;
        [FindsBy(How = How.CssSelector, Using = "button[name=\"add_cart_product\"]")]
        internal IWebElement AddCartElement;

        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal ProductPage Open()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart");
            return this;
        }

        internal void OpenProductPageByIndex(int productIndex)
        {
            driver.FindElements(By.CssSelector("div#box-most-popular li.product"))[productIndex].FindElement(By.CssSelector("a")).Click();
        }

        internal int GetProductCountInCart()
        {
            return int.Parse(CartQuantityElement.Text);
        }

        internal bool ContainsSizeSelector()
        {
            return (driver.FindElements(By.CssSelector("select[name=\"options[Size]\"]")).Count > 0);
        }

        internal void SelectSize(int sizeIndex)
        {
            SelectElement sizeSelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=\"options[Size]\"]")));
            sizeSelect.SelectByIndex(sizeIndex);
        }

        internal ProductPage AddProductToCart(int prevProductCount)
        {            
            AddCartElement.Click();
            wait.Until((d) => GetProductCountInCart() > prevProductCount);
            return this;
        }
    }
}
