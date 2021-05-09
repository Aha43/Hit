using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Worlds;
using System.Linq;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests3_space
    {
        private static IUnitTestsSpace<World3> _unitTestsSpace = new UnitTestsSpace<World3>();

        [Fact]
        public void ShouldHaveDimensionThree() => Assert.Equal(3, _unitTestsSpace.Dimension);
        [Fact]
        public void NumberOfSystemsShouldBeTwo() => Assert.Equal(2, _unitTestsSpace.SystemCount);
        [Fact]
        public void NumberOfLayersShouldBeThree() => Assert.Equal(3, _unitTestsSpace.LayerCount);
        
        [Theory]
        [InlineData("System3_1")]
        [InlineData("System3_2")]
        public void SystemNamesShouldContainSystem(string system) => Assert.Contains(system, _unitTestsSpace.SystemNames);

        [Theory]
        [InlineData("System3_1")]
        [InlineData("System3_2")]
        public void LayerNamesShouldContainLayer(string system) => Assert.Contains(system, _unitTestsSpace.SystemNames);

        [Theory]
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public void ShouldBeAbleToGetUnitTests(string system, string layer) => Assert.NotNull(_unitTestsSpace.GetUnitTests(system, layer));

        [Theory]
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public void NumberOfUnitTestsShouldBe10(string system, string layer) => Assert.Equal(5, _unitTestsSpace.GetUnitTests(system, layer).UnitTestCount);

        [Fact]
        public void NumberOfUnitTestCoordinatesShouldBeFifteen() => Assert.Equal(15, _unitTestsSpace.UnitTestCoordinates.Count());

        [Theory]
        [InlineData("System3_1", "Layer2", "testA_3")]
        [InlineData("System3_1", "Layer2", "testA_1_1")]
        [InlineData("System3_1", "Layer2", "testA_3_1")]
        [InlineData("System3_1", "Layer2", "testB_2")]
        [InlineData("System3_1", "Layer2", "testC_2")]

        [InlineData("System3_1", "Layer3", "testA_3")]
        [InlineData("System3_1", "Layer3", "testA_1_1")]
        [InlineData("System3_1", "Layer3", "testA_3_1")]
        [InlineData("System3_1", "Layer3", "testB_2")]
        [InlineData("System3_1", "Layer3", "testC_2")]

        [InlineData("System3_2", "Layer1", "testA_3")]
        [InlineData("System3_2", "Layer1", "testA_1_1")]
        [InlineData("System3_2", "Layer1", "testA_3_1")]
        [InlineData("System3_2", "Layer1", "testB_2")]
        [InlineData("System3_2", "Layer1", "testC_2")]
        public void CoordinateForUnitTestShouldBeFound(string system, string layer, string unitTest) => Assert.Contains((system, layer, unitTest), _unitTestsSpace.UnitTestCoordinates);
        
    }

}
