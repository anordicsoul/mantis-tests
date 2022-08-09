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
                Password = "root"
            };
            if (app.API.GetAllProjects(account).Count == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = GenerateRandomString(15),
                    Description = GenerateRandomString(100),
                };
                app.API.CreateNewProject(account, project);
            }
            List<ProjectData> oldList = app.API.GetAllProjects(account);

            app.projectManagementHelper.Remove(0);

            List<ProjectData> newList = app.API.GetAllProjects(account);
            ProjectData toBeRemoved = oldList[0];
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();

            foreach (ProjectData project in newList)
            {
                Assert.AreNotEqual(project.Id, toBeRemoved.Id);
            }

            Assert.AreEqual(oldList.Count, newList.Count);
            Assert.AreEqual(oldList, newList);
        }
    }
}
