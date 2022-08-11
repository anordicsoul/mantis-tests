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

        public void Remove(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            client.mc_project_delete(account.Name, account.Password, project.Id);
        }

        private void AcceptRemoveProject()
        {
            driver.FindElement(By.CssSelector("form.center input[type=\"submit\"]")).Click();
        }

        private void SubmitRemoveProjectButton()
        {
            driver.FindElement(By.Id("project-delete-form")).Click();
        }

        private void InitProjectModification(int index)
        {
            driver.FindElement(By.XPath($"(//table/tbody)[1]/tr[{index + 1}]/td/a")).Click();
        }

        public List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> projectList = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] apiProjects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            foreach (Mantis.ProjectData project in apiProjects)
            {
                projectList.Add(new ProjectData(project.name)
                {
                    Description = project.description,
                    Id = project.id
                });
            }
            return new List<ProjectData>(projectList);
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
