using Hit.Infrastructure;
using Hit.Specification.User;
using Items.HitIntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.IntegrationTests
{
    public class HitSpaceCoordinates : IHitSpaceCoordinates
    {
        private static readonly UnitTestsSpace<ItemCrudWorld> _testSpace = new();

        public HitSpaceCoordinates()
        {

        }

        public IEnumerable<(string system, string layer, string unitTests)> UnitTestCoordinates => _testSpace.UnitTestCoordinates;

    }

}
