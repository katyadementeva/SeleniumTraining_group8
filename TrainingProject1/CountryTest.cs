using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace TrainingProject1
{
    [TestFixture(Category = "Chrome")]
    public class CountryTest : BaseTest
    {
        public CountryTest() : base(Browser.Chrome) { }

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
                    Assert.True(country.CompareTo(prevCountry)>0, string.Format("Country {0} is shown before country {1}", country, prevCountry));
                    
                int zoneNum = int.Parse(countryRow.FindElements(By.CssSelector("td"))[zonesIndex].Text);
                if (zoneNum > 0)
                    countryWithZonesHrefs.Add(nameCell.FindElement(By.CssSelector("a")).GetAttribute("href"));
                prevCountry = country;
                rowIndex++;
            }
            
            foreach(string href in countryWithZonesHrefs)
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
    }
}
