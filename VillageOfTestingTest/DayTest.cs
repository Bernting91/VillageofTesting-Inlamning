using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting;

namespace VillageOfTestingTest
{
    public class DayTest : IClassFixture<VillageFixture>
    {
        private Village _village;

        public DayTest()
        {
            _village = new Village();

        }
        [Fact]
        public void DontConsumeFoodWithoutWorkers()
        {
            _village.Workers.Clear();
            _village.Food = 10;
            int initialFood = _village.Food;
            _village.Day();
            Assert.Equal(initialFood, _village.Food);
        }

        [Fact]
        public void FeedWorkerAndChangeDay()
        {
            _village.Food = 10;
            _village.AddWorker("Robert", "miner");
            int initialDaysGone = _village.DaysGone;
            _village.Day();
            Assert.True(_village.Food < 10);
            Assert.True(_village.DaysGone > initialDaysGone);
        }

        [Fact]
        public void NextDayWithoutFoodWithWorkers()
        {
            _village.Food = 0;
            _village.AddWorker("Robert", "miner");
            int initialDaysHungry = _village.Workers[0].DaysHungry;
            _village.Day();
            _village.Day();
            Assert.True(_village.Workers[0].DaysHungry > initialDaysHungry);
        }
        [Fact]
        public void IncreaseDaysGone()
        {
            int initialDaysGone = _village.DaysGone;
            _village.Day();
            Assert.Equal(initialDaysGone + 1, _village.DaysGone);
        }

        [Fact]
        public void IncreaseResources()
        {
            _village.Food = 0;
            _village.Wood = 0;
            _village.Metal = 0;
            _village.AddWorker("Lenny", "farmer");
            _village.AddWorker("Kenny", "lumberjack");
            _village.AddWorker("Benny", "miner");
            _village.Day();
            Assert.True(_village.Food > 0);
            Assert.True(_village.Wood > 0);
            Assert.True(_village.Metal > 0);
        }
        [Fact]
        public void HungerBeforeGameEnds()
        {
            _village.Food = 0;
            _village.AddWorker("Robert", "miner");
            int days = 0;

            while (!_village.GameOver)
            {
                _village.Day();
                days++;
            }
            int expectedDays = 6;
            Assert.Equal(expectedDays, days);
        }
    }
}
