using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using System.Linq;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests2_space
    {
        private static readonly IUnitTestsSpace<World2> _unitTestsSpace = new UnitTestsSpace<World2>();

        [Fact]
        public void ShouldHaveDimensionTwo() => Assert.Equal(2, _unitTestsSpace.Dimension);
        [Fact]
        public void NumberOfSystemsShouldBeTwo() => Assert.Equal(2, _unitTestsSpace.SystemCount);
        [Fact]
        public void NumberOfLayersShouldBeOne() => Assert.Equal(1, _unitTestsSpace.LayerCount);
        
        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public void SystemNamesShouldContainSystem(string system) => Assert.Contains(system, _unitTestsSpace.SystemNames);
        
        [Fact]
        public void LayerNamesShouldBeDefaultTheEmptyString() => Assert.Equal(string.Empty, _unitTestsSpace.LayerNames.First());

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public void ShouldBeAbleToGetUnitTests(string system) => Assert.NotNull(_unitTestsSpace.GetUnitTests(system));

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public void NumberOfUnitTestsShouldBe10(string system) => Assert.Equal(5, _unitTestsSpace.GetUnitTests(system).UnitTestCount);

        [Fact]
        public void NumberOfUnitTestCoordinatesShouldBeTen() => Assert.Equal(10, _unitTestsSpace.UnitTestCoordinates.Count());

        [Theory]
        [InlineData("System2_1", "", "testA_3")]
        [InlineData("System2_1", "", "testA_1_1")]
        [InlineData("System2_1", "", "testA_3_1")]
        [InlineData("System2_1", "", "testB_2")]
        [InlineData("System2_1", "", "testC_2")]
        [InlineData("System2_2", "", "testA_3")]
        [InlineData("System2_2", "", "testA_1_1")]
        [InlineData("System2_2", "", "testA_3_1")]
        [InlineData("System2_2", "", "testB_2")]
        [InlineData("System2_2", "", "testC_2")]
        public void CoordinateForUnitTestShouldBeFound(string system, string layer, string unitTest) => Assert.Contains((system, layer, unitTest), _unitTestsSpace.UnitTestCoordinates);
        
    }

}
