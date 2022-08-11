using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using mantis_tests.Mantis;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager)
        {

        }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }

        public void CreateNewProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            project.description = projectData.Description;
            client.mc_project_add(account.Name, account.Password, project);
        }

        public void DeleteProject(AccountData account, ProjectData project)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            client.mc_project_delete(account.Name, account.Password, project.Id);
        }

        public List<ProjectData> GetAllProjects(AccountData account)
        {
            List<ProjectData> projectList = new List<ProjectData>();

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach (Mantis.ProjectData project in projects)
            {
                projectList.Add(new ProjectData("test")
                {
                    Name = project.name,
                    Description = project.description,
                    Id = project.id
                });
            }

            return projectList;
        }
    }
}
