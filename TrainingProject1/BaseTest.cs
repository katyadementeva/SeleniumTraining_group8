using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Remote;
using System.IO;

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
        private Browser browser;
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
        }

        [TearDown]
        public void StopTest()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
