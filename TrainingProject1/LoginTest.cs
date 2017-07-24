using NUnit.Framework;
using TrainingProject1.app;

namespace TrainingProject1
{
    [TestFixture(Browser.Chrome, Category = "Chrome")]
    [TestFixture(Browser.IE, Category = "IE")]
    [TestFixture(Browser.FirefoxNew, Category ="FirefoxNew")]
    [TestFixture(Browser.FirefoxOld, Category = "FirefoxOld")]
    [TestFixture(Browser.FirefoxNightly, Category = "FirefoxNightly")]
    public class LoginTest:OldBaseTest
    {
        public LoginTest(Browser browser) : base(browser) { }

        [Test]
        public void TestLogin()
        {
            LoginToAdmin("admin", "admin");            
        }        
    }    
}
