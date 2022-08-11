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
                app.API.CreateNewProject(account, new ProjectData("TestProject"));
            }
            List<ProjectData> oldList = app.API.GetAllProjects(account);

            ProjectData toBeRemoved = oldList[0];

            app.projectManagementHelper.Remove(account, toBeRemoved);

            List<ProjectData> newList = app.API.GetAllProjects(account);

            Assert.AreEqual(oldList.Count - 1, newList.Count());
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}