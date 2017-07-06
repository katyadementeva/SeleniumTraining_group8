using NUnit.Framework;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace TrainingProject1
{
    [TestFixture(Browser.Chrome, Category = "Chrome")]
    [TestFixture(Browser.IE, Category = "IE")]
    [TestFixture(Browser.FirefoxNew, Category = "FirefoxNew")]
    [TestFixture(Browser.FirefoxOld, Category = "FirefoxOld")]
    [TestFixture(Browser.FirefoxNightly, Category = "FirefoxNightly")]
    public class ProductTest : BaseTest
    {
        public ProductTest(Browser browser) : base(browser) { }

        [Test]
        public void TestStickers()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart");
            ReadOnlyCollection<IWebElement> allProducts = driver.FindElements(By.CssSelector("li.product"));
            foreach (IWebElement product in allProducts)
            {
                Assert.True(product.FindElements(By.CssSelector("div.sticker")).Count.Equals(1));
            }
        }

        [Test]
        public void TestProductNames()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart");
            string[] productsSections = new string[] { "div#box-most-popular", "div#box-campaigns", "div#box-latest-products" };
            foreach (string productSection in productsSections)
            {
                ReadOnlyCollection<IWebElement> allProductElements = driver.FindElements(By.CssSelector(productSection + " li.product"));

                List<string> productHrefs = new List<string>();
                foreach (IWebElement productElement in allProductElements)
                {
                    productHrefs.Add(productElement.FindElement(By.CssSelector("a")).GetAttribute("href"));
                }
                foreach (string productHref in productHrefs)
                {
                    IWebElement productElement = driver.FindElement(By.CssSelector(productSection + " a[href = \"" + productHref + "\"]"));
                    string mpName = productElement.FindElement(By.CssSelector("div.name")).Text;
                    driver.Navigate().GoToUrl(productHref);
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-product")));

                    string ppName = driver.FindElement(By.CssSelector("h1.title")).Text;
                    Assert.True(mpName.Equals(ppName), string.Format("Different product name is shown on product page for product {0}: {1} in section {2}", mpName, ppName, productSection));
                    driver.Navigate().Back();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector(productSection)));                   
                }
            }
        }

        [Test]
        public void TestCompaignProduct()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart");
            IWebElement productElement = driver.FindElement(By.CssSelector("div#box-campaigns li.product"));
            IWebElement mpRegularPriceElement = productElement.FindElement(By.CssSelector("s.regular-price"));
            IWebElement mpActionPriceElement = productElement.FindElement(By.CssSelector("strong.campaign-price"));
            string mpName = productElement.FindElement(By.CssSelector("div.name")).Text;
            string mpRegularPrice = mpRegularPriceElement.Text;
            string mpActionPrice = mpActionPriceElement.Text;
            Assert.True(double.Parse(mpRegularPrice.Replace("$", "")) > double.Parse(mpActionPrice.Replace("$", "")), string.Format("Action price {0} is higher than regular price {1} for product {2}", mpActionPrice, mpRegularPrice, mpName));
            CheckCssForPrices(mpRegularPriceElement, mpActionPriceElement);

            productElement.FindElement(By.CssSelector("a")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-product")));
            IWebElement ppRegularPriceElement = driver.FindElement(By.CssSelector("div#box-product s.regular-price"));
            IWebElement ppActionPriceElement = driver.FindElement(By.CssSelector("div#box-product strong.campaign-price"));
            string ppName = driver.FindElement(By.CssSelector("h1.title")).Text;
            string ppRegularPrice = ppRegularPriceElement.Text;
            string ppActionPrice = ppActionPriceElement.Text;

            Assert.True(mpName.Equals(ppName), string.Format("Different product name is shown on product page for product {0}: {1}", mpName, ppName));
            Assert.True(mpRegularPrice.Equals(ppRegularPrice), string.Format("Different regular price is shown on product page for product {0}: {1} vs {2}", mpName, mpRegularPrice, ppRegularPrice));
            Assert.True(mpActionPrice.Equals(ppActionPrice), string.Format("Different action price is shown on product page for product {0}: {1} vs {2}", mpName, mpActionPrice, ppActionPrice));
            CheckCssForPrices(ppRegularPriceElement, ppActionPriceElement);
        }

        private void CheckCssForPrices(IWebElement regularPriceElement, IWebElement actionPriceElement)
        {
            Color regularPriceColor = ParseColor(regularPriceElement.GetCssValue("color"));
            Assert.True((regularPriceColor.G == regularPriceColor.B && regularPriceColor.G == regularPriceColor.R && regularPriceColor.B > 0), "Color of regular price is not grey");
            string regularPriceLine = regularPriceElement.GetCssValue("text-decoration");
            if (!regularPriceLine.Equals("line-through"))
                Assert.Inconclusive("Test can't detect if regular price is crossed out in browser " + browser.ToString() + ", please check manually " + driver.Url + " (" + regularPriceLine + ")");
            Size regularPriceSize = regularPriceElement.Size;

            Color actionPriceColor = ParseColor(actionPriceElement.GetCssValue("color"));
            Assert.True((actionPriceColor.G == 0 && actionPriceColor.B == 0 && actionPriceColor.R > 0), "Color of action price is not red");
            string actionPriceBold = actionPriceElement.GetCssValue("font-weight");
            if (!actionPriceBold.Equals("bold")&& !actionPriceBold.Equals("900") && !actionPriceBold.Equals("700")) 
                Assert.Inconclusive("Test can't detect if action price is bold in browser " + browser.ToString() + ", please check manually " + driver.Url + " (" + actionPriceBold + ")");
            Size actionPriceSize = actionPriceElement.Size;
            Assert.True(regularPriceSize.Height < actionPriceSize.Height && regularPriceSize.Width < actionPriceSize.Width, "Size of action price smaller than size of regular price");
        }
    }
}
