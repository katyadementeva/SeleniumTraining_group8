using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;
using TrainingProject1.app;

namespace TrainingProject1
{
    [TestFixture(Category = "Chrome")]
    public class MenuTest : OldBaseTest
    {
        public MenuTest() : base(Browser.Chrome) { }

        [Test]
        public void TestMenu()
        {
            LoginToAdmin("admin", "admin");
            Dictionary<string, string> menuItems = GetMenuDictionary("ul#box-apps-menu li[id='app-']>a");
            foreach (string menuItemName in menuItems.Keys)
            {
                ClickMenuByName(menuItems, menuItemName);
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));
                Dictionary <string, string> subMenuItems = GetMenuDictionary("li.selected ul a");
                foreach (string subMenuItemName in subMenuItems.Keys)
                {
                    ClickMenuByName(subMenuItems, subMenuItemName);
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));
                }
            }
        }       

    }    
}
