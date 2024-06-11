using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting;

namespace VillageOfTestingTest
{
    public class AddProjectTest : IClassFixture<VillageFixture>
    {
        private Village _village;

        public AddProjectTest()
        {
            _village = new Village();
            _village.AddWorker("Robert", "builder");

        }
        [Fact]
        public void AddProject()
        {
            _village.Projects.Clear();
            _village.Wood = 10;
            _village.Metal = 10;
            string projectName = "House";
            _village.AddProject(projectName);
            Assert.Contains(_village.Projects, p => p.Name == projectName);
        }

        [Fact]
        public void AddInvalidProject()
        {
            _village.Projects.Clear();
            string invalidProjectName = "InvalidProject";
            _village.AddProject(invalidProjectName);
            Assert.DoesNotContain(_village.Projects, p => p.Name == invalidProjectName);
        }

        [Fact]
        public void AddProjectWithoutMaterial()
        {
            _village.Projects.Clear();
            _village.Wood = 0;
            _village.Metal = 0;
            string projectName = "House";
            _village.AddProject(projectName);
            Assert.DoesNotContain(_village.Projects, p => p.Name == projectName);
        }

        [Fact]
        public void AddCastleProjectToWin()
        {
            _village.Projects.Clear();
            _village.AddWorker("Robert", "builder");
            _village.AddWorker("Karl", "farmer");
            _village.AddWorker("Charles", "miner");
            _village.AddWorker("Gnarles", "lumberjack");

            string projectName = "Castle";

            while (_village.Wood < 50 || _village.Metal < 50)
            {
                _village.Day();
            }

            if (_village.Wood >= 50 && _village.Metal >= 50)
            {
                _village.AddProject(projectName);
                Assert.Contains(_village.Projects, p => p.Name == projectName);
            }

            int maxDays = 51;
            for (int i = 0; i < maxDays && _village.Projects.Any(p => p.Name == projectName && p.DaysLeft > 0); i++)
            {
                _village.Day();
            }

            Assert.True(_village.GameOver);
        }
        [Fact]
        public void TestQuarryYieldIncrease()
        {
            _village.Projects.Clear();
            _village.Wood = 100;
            _village.Metal = 100;
            int initialMetalPerDay = _village.MetalPerDay;
            _village.AddProject("Quarry");
            while (_village.Projects.Any(p => p.Name == "Quarry" && p.DaysLeft > 0))
            {
                _village.Day();
            }
            Assert.True(_village.MetalPerDay > initialMetalPerDay);
        }
        [Fact]
        public void TestWoodmillYieldIncrease()
        {
            _village.Projects.Clear();
            _village.Wood = 100;
            _village.Metal = 100;
            int initialWoodPerDay = _village.WoodPerDay;
            _village.AddProject("Woodmill");
            while (_village.Projects.Any(p => p.Name == "Woodmill" && p.DaysLeft > 0))
            {
                _village.Day();
            }
            Assert.True(_village.WoodPerDay > initialWoodPerDay);
        }
        [Fact]
        public void TestFarmYieldIncrease()
        {
            _village.Projects.Clear();
            _village.Wood = 100;
            _village.Metal = 100;
            int initalFoodPerDay = _village.FoodPerDay;
            _village.AddProject("Farm");
            while (_village.Projects.Any(p => p.Name == "Farm" && p.DaysLeft > 0))
            {
                _village.Day();
            }
            Assert.True(_village.FoodPerDay > initalFoodPerDay);
        }


    }
}
