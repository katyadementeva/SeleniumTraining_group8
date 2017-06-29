using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Collections;

namespace TrainingProject1
{
    [TestFixture(Category = "Chrome")]
    public class MenuTest : BaseTest
    {
        public MenuTest() : base(Browser.Chrome) { }

        [Test]
        public void TestMenu()
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart/admin");
            Login("admin", "admin");
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.notice.success")));
            IWebElement menu = driver.FindElement(By.CssSelector("ul#box-apps-menu"));
            ReadOnlyCollection<IWebElement> menuItems = menu.FindElements(By.CssSelector("li[id='app-'] a"));
            ArrayList menuItemHrefs = new ArrayList();
            foreach (IWebElement menuItem in menuItems)
            {
                menuItemHrefs.Add(menuItem.GetAttribute("href"));
            }
            foreach (string href in menuItemHrefs)
            {
                driver.FindElement(By.CssSelector("[href=\"" + href + "\"]")).Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));
                ReadOnlyCollection<IWebElement> subMenuItems = driver.FindElements(By.CssSelector("li.selected ul a"));
                ArrayList subMenuItemHrefs = new ArrayList();
                foreach (IWebElement subMenuItem in subMenuItems)
                {
                    subMenuItemHrefs.Add(subMenuItem.GetAttribute("href"));
                }
                foreach (string hrefSub in subMenuItemHrefs)
                {
                    driver.FindElement(By.CssSelector("[href=\"" + hrefSub + "\"]")).Click();
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));
                }
            }
        }
    }    
}
