using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Data;

namespace HD_Scenario
{
    [TestClass]
    public class TestScripts
    {
        IWebDriver driver;

        [TestInitialize()]

        public void Initilizer_LaunchBrowser()
        {
            string BrowserURL, ChromeDriverPath;
            BrowserURL = TestContext.DataRow["BrowserURL"].ToString();
            ChromeDriverPath = TestContext.DataRow["ChromeDriverPath"].ToString();

            driver = new ChromeDriver(ChromeDriverPath);
            driver.Navigate().GoToUrl(BrowserURL);            
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\HD_TestData.csv", "HD_TestData#csv", DataAccessMethod.Sequential), DeploymentItem("DataSource\\HD_TestData.csv"), DeploymentItem("HD_TestData.csv")]
        public void FirstTestScript()
        {
            string SearchText;
            SearchText = TestContext.DataRow["SearchText"].ToString();

            IWebElement searchBoxs = driver.FindElement(By.Id("gcc-inline-search"));
            searchBoxs.SendKeys(SearchText);
            searchBoxs.SendKeys(Keys.Enter);
            driver.FindElement(By.CssSelector("#gcc-search-results > div.flex.justify-between.items-center.pv3-l.mt3.mt0-ns > div.tr > div.dn.db-ns.f7.f6-l > ul > li:nth-child(4) > a")).Click();
            Thread.Sleep(7000);
            IList<IWebElement> discountPrice =  driver.FindElements(By.CssSelector("[data-testid='gcc-product-discount-price']"));

            int i, j;
            for (i = 0 ; i <= discountPrice.Count; i++)
            {
                if (i == discountPrice.Count)
                {
                    j = 0;
                }
                else
                {
                    j = 1;
                }
                if (Convert.ToInt64(Convert.ToDouble(SplitFunction.SplitFunctionValueDollar(discountPrice[i].Text))) <= Convert.ToInt64(Convert.ToDouble( SplitFunction.SplitFunctionValueDollar(discountPrice[i+j].Text))))
                {
                    Console.WriteLine("As expected, Sort order is in Ascending format " + discountPrice[i].Text);                    
                }
                else
                {                    
                    Assert.Fail("Need Defect, Unable to perform sorting function for Ascending order. Reason : Product With cost "+ discountPrice[i].Text +" is shown ahead of product with Cost "+ discountPrice[i+j].Text);
                    break;
                }
            }
        }

        [TestCleanup]
        public void MyTestCleanUP()
        {
            driver.Quit();
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
