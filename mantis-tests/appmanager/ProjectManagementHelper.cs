using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }
        public void Create(ProjectData project)
        {
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreationForm();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.XPath("(//table/tbody)[1]/tr")).Count > 0);
        }
        public void Remove(int index)
        {
            manager.Navigation.GoToManageOverviewPage();
            manager.Navigation.GoToProjectControlPage();
            InitProjectModification(index);
            SubmitRemoveProjectButton();
            AcceptRemoveProject();
        }

        private void AcceptRemoveProject()
        {
            driver.FindElement(By.CssSelector("form.center input[type=\"submit\"]")).Click();
        }

        private void SubmitRemoveProjectButton()
        {
            driver.FindElement(By.CssSelector("form#project-delete-form input[type=\"submit\"]")).Click();
        }

        private void InitProjectModification(int index)
        {
            driver.FindElement(By.XPath($"(//table/tbody)[1]/tr[{index + 1}]/td/a")).Click();
        }

        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projectList = new List<ProjectData>();

            ICollection<IWebElement> elements = driver.FindElements(By.XPath("(//table/tbody)[1]/tr"));
            foreach (IWebElement element in elements)
            {
                projectList.Add(new ProjectData()
                {
                    Id = Regex.Match(element.FindElement(By.XPath("./td[1]/a")).GetAttribute("href"), "\\d+").Value,
                    Name = element.FindElement(By.XPath("./td[1]")).Text,
                    Description = element.FindElement(By.XPath("./td[5]")).Text
                });
            }
            return projectList;
        }
        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//form[@action=\"manage_proj_create_page.php\"]//button[@type=\"submit\"]")).Click();
        }
        public void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Name("description")).SendKeys(project.Description);
        }
        public void SubmitProjectCreationForm()
        {
            driver.FindElement(By.XPath("//input[@type=\"submit\"]")).Click();
        }
    }
}
