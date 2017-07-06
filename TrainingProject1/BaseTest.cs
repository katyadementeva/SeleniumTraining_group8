using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;

namespace TrainingProject1
{
    public enum Browser
    {
        IE, 
        Chrome,
        FirefoxNew,
        FirefoxOld, 
        FirefoxNightly
    }

    public abstract class BaseTest
    {
        internal Browser browser;
        internal IWebDriver driver;
        internal WebDriverWait wait;

        public BaseTest(Browser nBrowser)
        {
            browser = nBrowser;
        }

        [SetUp]
        public void StartTest()
        {            
            switch (browser)
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);

        }

        [TearDown]
        public void StopTest()
        {
            driver.Quit();
            driver.Dispose();
        }

        internal void LoginToAdmin(string name, string password)
        {
            driver.Navigate().GoToUrl("http://localhost:8081/litecart/admin");
            driver.FindElement(By.CssSelector("input[name=username]")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.notice.success")));
        }

        internal Dictionary<string, string> GetMenuDictionary(string menuCssSelector)
        {
            ReadOnlyCollection<IWebElement> menuItemElements = driver.FindElements(By.CssSelector(menuCssSelector));
            Dictionary<string, string> menuItems = new Dictionary<string, string>();
            foreach (IWebElement menuItemElement in menuItemElements)
            {
                menuItems.Add(menuItemElement.Text, menuItemElement.GetAttribute("href"));
            }
            return menuItems;
        }

        internal void ClickMenuByName(Dictionary<string, string> menuItems, string menuItemName)
        {
            driver.FindElement(By.CssSelector("[href=\"" + menuItems[menuItemName] + "\"]")).Click();
        }

        internal Dictionary<string, int> GetTableHeaders(string tableCssSelector)
        {
            ReadOnlyCollection<IWebElement> tableHeaderElments = driver.FindElements(By.CssSelector(tableCssSelector + " tr.header th"));
            int i = 0;
            Dictionary<string, int> headers = new Dictionary<string, int>();

            foreach (IWebElement tableHeaderElement in tableHeaderElments)
            {
                string text = tableHeaderElement.Text;
                if (text.Length > 0)
                {
                    headers.Add(text, i);
                }
                i++;
            }
            return headers;
        }

        internal Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb")) ///rgb or argb
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hexa color string");
        }
    }
}
