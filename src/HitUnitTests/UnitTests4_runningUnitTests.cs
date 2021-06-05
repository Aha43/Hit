using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Worlds;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests4_runningUnitTests
    {
        private static readonly IUnitTestsSpace<World4> _unitTestsSpace = new UnitTestsSpace<World4>();

        [Theory]
        [InlineData("system-with-configuration-no-section-1", "AppSettingTest1")]
        [InlineData("system-with-configuration-no-section-2", "AppSettingTest2")]
        public async Task UnitTestShouldNotFailAsync(string system, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
            Assert.NotNull(result);
            Assert.True(result.Success());
            Assert.Equal(unitTest, result.UnitTest);
            Assert.Equal(system, result.System);
        }

    }

}
