using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class ProjectCreationTests : ProjectManagementTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData("TestProject")
            {
                Description = GenerateRandomString(100)
            };

            List<ProjectData> oldList = app.API.GetAllProjects(account);

            app.projectManagementHelper.Create(project);

            List<ProjectData> newList = app.API.GetAllProjects(account);

            oldList.Add(project);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList.Count, newList.Count);
            Assert.AreEqual(oldList, newList);
        }
    }
}
