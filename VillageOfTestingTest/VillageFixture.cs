using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageOfTesting;

namespace VillageOfTestingTest
{
    public class VillageFixture : IDisposable
    {
        public Village Village { get; private set; }

        public VillageFixture()
        {
            Village = new Village();
        }

        public void Dispose()
        {
        }
    }
}
