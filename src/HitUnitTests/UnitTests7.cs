using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests7
    {
        private static readonly IUnitTestsSpace<World7> _unitTestsSpace = new UnitTestsSpace<World7>();

        [Theory]
        [InlineData("system-with-configuration-sections-file-and-user", "AppSettingTest1")]
        public async Task UnitTestShouldNotFailAsync(string system, string unitTest)
        {
            if (UnitTestsUtil.InDevelopment)
            {
                var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
                UnitTestsUtil.AssertResult(result, system, string.Empty, unitTest);
            }
        }
    }
}
