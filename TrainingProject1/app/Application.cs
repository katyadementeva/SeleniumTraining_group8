using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using TrainingProject1.page;

namespace TrainingProject1.app
{    
    public class Application
    {
        private Browser browser;
        private IWebDriver driver;

        private HomePage homePage;
        private ProductPage productPage;
        private CartPage cartPage;

        public Application(Browser browser)
        {
            this.browser = browser;
            switch (this.browser)
            {
                case Browser.Chrome:
                    ChromeOptions chromeOpt = new ChromeOptions();
                    chromeOpt.AddArgument("start-maximized");
                    driver = new ChromeDriver(chromeOpt);
                    break;
                case Browser.IE:
                    InternetExplorerOptions ieOpt = new InternetExplorerOptions();
                    ieOpt.RequireWindowFocus = true;
                    driver = new InternetExplorerDriver(ieOpt);
                    break;
                case Browser.FirefoxNew:
                    FirefoxOptions firefoxOptNew = new FirefoxOptions();
                    firefoxOptNew.UseLegacyImplementation = false;
                    driver = new FirefoxDriver(firefoxOptNew);
                    break;
                case Browser.FirefoxOld:
                    FirefoxOptions firefoxOptOld = new FirefoxOptions();
                    firefoxOptOld.UseLegacyImplementation = true;
                    firefoxOptOld.BrowserExecutableLocation = @"D:\apps\Mozilla Firefox ESR\firefox.exe";
                    driver = new FirefoxDriver(firefoxOptOld);
                    break;
                case Browser.FirefoxNightly:
                    FirefoxOptions firefoxOptNightly = new FirefoxOptions();
                    firefoxOptNightly.UseLegacyImplementation = false;
                    firefoxOptNightly.BrowserExecutableLocation = @"C:\Program Files\Nightly\firefox.exe";
                    driver = new FirefoxDriver(firefoxOptNightly);
                    break;
            }
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);
            homePage = new HomePage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }
        
        public void Quit()
        {
            driver.Quit();
        }

        internal void AddProductToCartByIndex(int productIndex)
        {
            homePage.Open().OpenProductPageByIndex(productIndex);
            int prevCount = productPage.GetProductCountInCart();
            if (productPage.ContainsSizeSelector())
                productPage.SelectSize(1);
            productPage.AddProductToCart(prevCount);
        }

        internal void RemoveAllProductsFromCart()
        {
            cartPage.Open();
            while (cartPage.GetProductCount() > 0)
            {
                cartPage.RemoveProduct();               
            }

        }
    }
}
