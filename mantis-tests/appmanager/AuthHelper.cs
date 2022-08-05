using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;

namespace mantis_tests
{
    public class AuthHelper : HelperBase
    {
        public AuthHelper(ApplicationManager manager) : base(manager) { }


        public void Autenticate(AccountData account)
        {
            FillLogin(account);
            SubmitLoginButton();
            FillPassword(account);
            SubmitLoginButton();
        }
        public void FillLogin(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
        }
        public void FillPassword(AccountData account)
        {
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
        }
        private void SubmitLoginButton()
        {
            driver.FindElement(By.XPath("//input[@type=\"submit\"]")).Click();
        }
    }
}
