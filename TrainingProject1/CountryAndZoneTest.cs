using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrainingProject1.app;

namespace TrainingProject1
{
    [TestFixture(Category = "Chrome")]
    public class CountryAndZoneTest : OldBaseTest
    {
        public CountryAndZoneTest() : base(Browser.Chrome) { }

        [Test]
        public void TestCountry()
        {
            LoginToAdmin("admin", "admin");
            Dictionary<string, string> menuItems = GetMenuDictionary("ul#box-apps-menu li[id='app-']>a");
            ClickMenuByName(menuItems, "Countries");
            Dictionary<string, int> countryTableHeaders = GetTableHeaders("form[name=\"countries_form\"] table");
            int nameIndex = countryTableHeaders["Name"];
            int zonesIndex = countryTableHeaders["Zones"];
            string prevCountry = "";
            List<string> countryWithZonesHrefs = new List<string>();
            int rowIndex = 0;
            IReadOnlyCollection<IWebElement> countryRows = driver.FindElements(By.CssSelector("form[name=\"countries_form\"] table tr.row"));
            foreach (IWebElement countryRow in countryRows)
            {
                IWebElement nameCell = countryRow.FindElements(By.CssSelector("td"))[nameIndex];
                string country = nameCell.Text;
                if (prevCountry == "")
                    prevCountry = country;
                else
                    Assert.True(country.CompareTo(prevCountry) > 0, string.Format("Country {0} is shown before country {1}", country, prevCountry));

                int zoneNum = int.Parse(countryRow.FindElements(By.CssSelector("td"))[zonesIndex].Text);
                if (zoneNum > 0)
                    countryWithZonesHrefs.Add(nameCell.FindElement(By.CssSelector("a")).GetAttribute("href"));
                prevCountry = country;
                rowIndex++;
            }

            foreach (string href in countryWithZonesHrefs)
            {
                driver.Navigate().GoToUrl(href);
                Dictionary<string, int> zonesTableHeaders = GetTableHeaders("table#table-zones");
                int zoneNameIndex = zonesTableHeaders["Name"];
                List<string> zonesList = new List<string>();
                IReadOnlyCollection<IWebElement> zoneRows = driver.FindElements(By.CssSelector("table#table-zones tr:not(.header)"));
                string prevZone = "";
                foreach (IWebElement zoneRow in zoneRows)
                {
                    IWebElement zoneNameCell = zoneRow.FindElements(By.CssSelector("td"))[zoneNameIndex];
                    if (!zoneNameCell.FindElement(By.CssSelector("input")).Displayed)
                    {
                        string zone = zoneNameCell.Text;
                        if (prevZone == "")
                            prevZone = zone;
                        else
                            Assert.True(zone.CompareTo(prevZone) > 0, string.Format("Zone {0} is shown before zone {1} for this href '{2}'", zone, prevZone, href));
                        prevZone = zone;
                    }
                }
                driver.Navigate().Back();
            }
        }
        [Test]
        public void TestGeoZones()
        {
            LoginToAdmin("admin", "admin");
            Dictionary<string, string> menuItems = GetMenuDictionary("ul#box-apps-menu li[id='app-']>a");
            ClickMenuByName(menuItems, "Geo Zones");
            Dictionary<string, int> countryTableHeaders = GetTableHeaders("form[name=\"geo_zones_form\"] table");
            int nameIndex = countryTableHeaders["Name"];
            IReadOnlyCollection<IWebElement> countryRows = driver.FindElements(By.CssSelector("form[name=\"geo_zones_form\"] table tr.row"));
            List<string> countryHrefs = new List<string>();
            foreach (IWebElement countryRow in countryRows)
            {
                IWebElement nameCell = countryRow.FindElements(By.CssSelector("td"))[nameIndex];
                countryHrefs.Add(nameCell.FindElement(By.CssSelector("a")).GetAttribute("href"));
            }

            foreach (string href in countryHrefs)
            {
                driver.Navigate().GoToUrl(href);
                Dictionary<string, int> zonesTableHeaders = GetTableHeaders("table#table-zones");
                int zoneNameIndex = zonesTableHeaders["Zone"];
                List<string> zonesList = new List<string>();
                IReadOnlyCollection<IWebElement> zoneRows = driver.FindElements(By.CssSelector("table#table-zones tr:not(.header)"));
                string prevZone = "";
                foreach (IWebElement zoneRow in zoneRows)
                {
                    if (zoneRow.FindElements(By.CssSelector("a#add_zone")).Count == 0)
                    {
                        IWebElement zoneNameCell = zoneRow.FindElements(By.CssSelector("td"))[zoneNameIndex];
                        string zone = zoneNameCell.FindElement(By.CssSelector("option[selected]")).Text;
                        if (prevZone == "")
                            prevZone = zone;
                        else
                            Assert.True(zone.CompareTo(prevZone) > 0, string.Format("Zone {0} is shown before zone {1} for this href '{2}'", zone, prevZone, href));
                        prevZone = zone;
                    }
                }

            }
        }
        [Test]
        public void TestCountryExternalLinks()
        {
            LoginToAdmin("admin", "admin");
            Dictionary<string, string> menuItems = GetMenuDictionary("ul#box-apps-menu li[id='app-']>a");
            ClickMenuByName(menuItems, "Countries");
            Dictionary<string, int> countryTableHeaders = GetTableHeaders("form[name=\"countries_form\"] table");
            int nameIndex = countryTableHeaders["Name"];
            ReadOnlyCollection<IWebElement> countryRows = driver.FindElements(By.CssSelector("form[name=\"countries_form\"] table tr.row"));
            Random r = new Random();
            int countryIndex = r.Next(countryRows.Count - 1);
            countryRows[countryIndex].FindElements(By.CssSelector("td"))[nameIndex].FindElement(By.CssSelector("a")).Click();
            ReadOnlyCollection<IWebElement> externalLinks = driver.FindElements(By.CssSelector("a[target=\"_blank\"]"));
            foreach (IWebElement externalLink in externalLinks)
            {
                string originalWindow = driver.CurrentWindowHandle;
                ReadOnlyCollection<string> existingWindows = driver.WindowHandles;
                externalLink.Click();
                wait.Until(d=> d.WindowHandles.Count>existingWindows.Count);
                string newWindow = AnyWindowOtherThan(existingWindows);
                driver.SwitchTo().Window(newWindow);
                driver.FindElement(By.CssSelector("body"));
                driver.Close();
                driver.SwitchTo().Window(originalWindow);

            }
        }

        private string AnyWindowOtherThan(ReadOnlyCollection<string> oldWindows)
        {
            ReadOnlyCollection<string> newWindows = driver.WindowHandles;
            IEnumerable<string> createdWindows = newWindows.Except(oldWindows);
            if (createdWindows.Count() > 0)
                return createdWindows.ElementAt(0);
            return null;

        }
    }
}
