using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]

    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void RemoveProjectTest()
        {
           AccountData account = new AccountData()
           {
               Name = "administrator",
               Password = "secret"
           };

           ProjectData project = new ProjectData()
           {
               Name = "Project" + DateTime.UtcNow.ToString(),
               Description = "Project Description " + DateTime.UtcNow.ToString()
           };

            List<ProjectData> projects = new List<ProjectData>();
            projects = app.API.GetAllProjects(account);

            if (app.API.GetAllProjects(account).Count == 0)
            {
               
                app.API.CreateNewProject(account, project);
                projects = app.API.GetAllProjects(account);
            }

            List<ProjectData> oldList = app.API.GetAllProjects(account);

            app.projectManagementHelper.Remove(0);

            List<ProjectData> projectsRemove = app.API.GetAllProjects(account);
            Assert.AreNotEqual(projectsRemove, projects);
            Assert.AreEqual((projectsRemove.Count + 1), projects.Count);
        }
    }
}