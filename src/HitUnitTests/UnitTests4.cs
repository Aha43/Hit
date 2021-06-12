using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests4
    {
        private static readonly IUnitTestsSpace<World4> _unitTestsSpace = new UnitTestsSpace<World4>();

        [Theory]
        [InlineData("system-with-configuration-no-sections-1", "AppSettingTest1")]
        [InlineData("system-with-configuration-no-sections-2", "AppSettingTest2")]
        [InlineData("system-with-configuration-sections-part1", "AppSettingTest3")]
        [InlineData("system-with-configuration-sections-part2", "AppSettingTest4")]
        public async Task UnitTestShouldNotFailAsync(string system, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
            UnitTestsUtil.AssertResult(result, system, string.Empty, unitTest);
            UnitTestsUtil.AssertResult(result, system, string.Empty, unitTest);
        }
    }
}
