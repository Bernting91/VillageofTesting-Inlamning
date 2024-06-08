using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting;

namespace VillageOfTestingTest
{
    public class AddWorkerTest : IClassFixture<VillageFixture>
    {
        private Village _village;

        public AddWorkerTest(VillageFixture fixture)
        {
            _village = fixture.Village;
        }
        [Fact]
        public void AddWorkerToOccupation()
        {

            _village.Workers.Clear();
            string workerName = "Robert";
            string occupation = "farmer";
            _village.AddWorker(workerName, occupation);
            Assert.Contains(_village.Workers, w => w.Name == workerName && w.Occupation == occupation);
        }
        [Fact]
        public void AddSeveralWorkerstoOccupation()
        {
            _village.Workers.Clear();
            string occupation = "farmer";
            string[] workerNames = { "Karl", "Robert", "Tim" };

            foreach (string workerName in workerNames)
            {
                _village.AddWorker(workerName, occupation);
                Assert.Contains(_village.Workers, w => w.Name == workerName && w.Occupation == occupation);

            }
        }


        [Theory]
        [InlineData("Bill", "farmer")]
        [InlineData("Bull", "miner")]
        [InlineData("Kenny", "lumberjack")]
        public void AddSeveralWorkersToOccupationWithTheory(string workerName, string occupation)
        {
            _village.Workers.Clear();
            _village.AddWorker(workerName, occupation);
            Assert.Contains(_village.Workers, w => w.Name == workerName && w.Occupation == occupation);
        }

        [Fact]
        public void AddWorkerToInvalidOccupation()
        {

            string workerName = "Robert";
            string invalidOccupation = "invalidOccupation";
            _village.AddWorker(workerName, invalidOccupation);
            Assert.DoesNotContain(_village.Workers, w => w.Name == workerName && w.Occupation == invalidOccupation);
        }

        [Fact]
        public void ExceedsMaxWorkers()
        {
            string workerName = "Robert";
            string occupation = "farmer";
            for (int i = 0; i < _village.MaxWorkers + 1; i++)
            {
                _village.AddWorker(workerName + i, occupation);
            }
            Assert.Equal(_village.MaxWorkers, _village.Workers.Count);
        }

    }
}
