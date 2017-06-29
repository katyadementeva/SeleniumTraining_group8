using NUnit.Framework;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace TrainingProject1
{
    [TestFixture(Category = "Chrome")]
    public class ProductTest : BaseTest
    {
        public ProductTest() : base(Browser.Chrome) { }

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
    }
}
