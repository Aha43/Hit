using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Worlds;
using System.Linq;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests1_space
    {
        private static readonly IUnitTestsSpace<World1> _unitTestsSpace = new UnitTestsSpace<World1>();

        [Fact]
        public void ShouldHaveDimensionOne() => Assert.Equal(1, _unitTestsSpace.Dimension);
        [Fact]
        public void NumberOfSystemsShouldBeOne() => Assert.Equal(1, _unitTestsSpace.SystemCount);
        [Fact]
        public void NumberOfLayersShouldBeOne() => Assert.Equal(1, _unitTestsSpace.LayerCount);
        [Fact]
        public void SystemNameShouldBeSystem1() => Assert.Equal("System1", _unitTestsSpace.SystemNames.First());
        [Fact]
        public void LayerNameShouldBeDefaultTheEmptyString() => Assert.Equal(string.Empty, _unitTestsSpace.LayerNames.First());
        [Fact]
        public void ShouldBeAbleToGetUnitTests1() => Assert.NotNull(_unitTestsSpace.GetUnitTests("System1"));
        [Fact]
        public void ShouldBeAbleToGetUnitTests2() => Assert.NotNull(_unitTestsSpace.GetUnitTests(""));
        [Fact]
        public void ShouldBeAbleToGetUnitTests3() => Assert.NotNull(_unitTestsSpace.GetUnitTests(null));
        [Fact]
        public void ShouldBeAbleToGetUnitTests4() => Assert.NotNull(_unitTestsSpace.GetUnitTests(null, null));
        [Fact]
        public void ShouldBeAbleToGetUnitTests5() => Assert.NotNull(_unitTestsSpace.GetUnitTests("", ""));
        [Fact]
        public void OneDimensionalSystemShouldBeNamedSystem1() => Assert.True(_unitTestsSpace.GetUnitTests() == _unitTestsSpace.GetUnitTests("System1"));
        [Fact]
        public void NumberOfUnitTestsShouldBeFive() => Assert.Equal(5, _unitTestsSpace.GetUnitTests().UnitTestCount);
        [Fact]
        public void NumberOfUnitTestCoordinatesShouldBeFive() => Assert.Equal(5, _unitTestsSpace.UnitTestCoordinates.Count());
        [Theory]
        [InlineData("System1", "", "testA_3")]
        [InlineData("System1", "", "testA_1_1")]
        [InlineData("System1", "", "testA_3_1")]
        [InlineData("System1", "", "testB_2")]
        [InlineData("System1", "", "testC_2")]
        public void CoordinateForUnitTestShouldBeFound(string system, string layer, string unitTest) => Assert.Contains((system, layer, unitTest), _unitTestsSpace.UnitTestCoordinates);
        [Theory]
        [InlineData("System", "", "testA_3")]
        [InlineData("System1", "allo", "testA_1_1")]
        [InlineData("System1", "", "hi")]
        public void CoordinateForUnitTestShouldNotBeFound(string system, string layer, string unitTest) => Assert.DoesNotContain((system, layer, unitTest), _unitTestsSpace.UnitTestCoordinates);
    }
}
