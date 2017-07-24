using TrainingProject1.app;
using NUnit.Framework;

namespace TrainingProject1.test
{
    public class BaseTest
    {
        public Application app;
        Browser browser;
        public BaseTest(Browser browser)
        {
            this.browser = browser;
        }

        [SetUp]
        public void start()
        {
            app = new Application(browser);
        }

        [TearDown]
        public void stop()
        {
            app.Quit();
            app = null;
        }
    }
}
