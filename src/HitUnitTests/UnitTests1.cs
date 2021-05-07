using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests1
    {
        private static IUnitTestsSpace<World1> _unitTestsSpace = new UnitTestsSpace<World1>();

        [Fact]
        public void ShouldHaveDimension1() => Assert.Equal(1, _unitTestsSpace.Dimension);
        [Fact]
        public void NumberOfSystemsShouldBeOne() => Assert.Equal(1, _unitTestsSpace.SystemCount);
        [Fact]
        public void NumberOfLayersShouldBeOne() => Assert.Equal(1, _unitTestsSpace.LayerCount);
        [Fact]
        public void SystemNameShouldBeSystem1() => Assert.Equal("System1", _unitTestsSpace.SystemNames.First());
        [Fact]
        public void LayerNameShouldBeDefaultTheEmptyString() => Assert.Equal(string.Empty, _unitTestsSpace.LayerNames.First());
        [Fact]
        public void OneDimensionalSystemShouldBeNamedSystem1() => Assert.True(_unitTestsSpace.GetUnitTests() == _unitTestsSpace.GetUnitTests("System1"));
        [Fact]
        public void NumberOfUnitTestsShouldBeThree() => Assert.Equal(3, _unitTestsSpace.GetUnitTests().UnitTestCount);

    }

}
