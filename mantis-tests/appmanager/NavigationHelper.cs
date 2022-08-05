using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        private string mantis_vers;

        public NavigationHelper(ApplicationManager manager, string baseURL, string mantis_vers) : base(manager)
        {
            this.baseURL = baseURL;
            this.mantis_vers = mantis_vers;
        }
        public NavigationHelper GoToHomePage()
        {
            if (driver.Url == baseURL)
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL);
            return this;
        }
        public NavigationHelper GoToManageOverviewPage()
        {
            driver.FindElement(By.XPath($"//a[@href='/mantisbt-{mantis_vers}/manage_overview_page.php']")).Click();

            return this;
        }
        public NavigationHelper GoToProjectControlPage()
        {
            driver.FindElement(By.XPath($"//a[@href='/mantisbt-{mantis_vers}/manage_proj_page.php']")).Click();

            return this;
        }
    }
}
