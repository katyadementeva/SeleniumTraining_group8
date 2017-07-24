using NUnit.Framework;
using TrainingProject1.app;

namespace TrainingProject1.test
{
    [TestFixture(Browser.Chrome, Category = "Chrome")]
    public class AddProductToCartTest: BaseTest
    {
        public AddProductToCartTest(Browser browser) : base(browser) { }
        [Test]
        public void TestProductInCart()
        {
            for (int i = 0; i < 3; i++)
            {
                app.AddProductToCartByIndex(0);                
            }
            app.RemoveAllProductsFromCart();           
        }
    }
}
