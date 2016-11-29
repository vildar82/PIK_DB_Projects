using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIK_DB_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects.Tests
{
    [TestClass()]
    public class MDMServiceTests
    {
        [TestMethod()]
        public void GetProjectsTest()
        {
            var projects = MDMService.GetProjects();
            Assert.AreEqual(projects.Count, 25);
        }

        [TestMethod()]
        public void GetHousesTest()
        {
            int parentId = 1;
            var houses = MDMService.GetHouses(parentId);

            Console.WriteLine($"ParentId={parentId}, домов {houses.Count}");

            Assert.IsTrue(houses.All(h => h.ObjectType == ObjectTypeEnum.House ||
                        (h.ObjectType == ObjectTypeEnum.Block && h.IsBuilding== true)));
        }
    }
}