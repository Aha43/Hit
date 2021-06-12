using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests6
    {
        private static readonly IUnitTestsSpace<World6> _unitTestsSpace = new UnitTestsSpace<World6>();

        [Theory]
        [InlineData("system-with-configuration-sections-multi", "AppSettingTest1")]
        public async Task UnitTestShouldNotFailAsync(string system, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
            UnitTestsUtil.AssertResult(result, system, string.Empty, unitTest);
        }
    }
}
