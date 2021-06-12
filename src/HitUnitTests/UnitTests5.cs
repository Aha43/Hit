using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTests5
    {
        private static readonly IUnitTestsSpace<World5> _unitTestsSpace = new UnitTestsSpace<World5>();

        [Theory]
        [InlineData("system-with-configuration-user-secret", "AppSettingUserSecretTest1")]
        [InlineData("system-with-configuration-user-secret-part1", "AppSettingUserSecretTest2")]
        [InlineData("system-with-configuration-user-secret-part2", "AppSettingUserSecretTest3")]
        public async Task UnitTestShouldUsingUserSecretNotFailAsync(string system, string unitTest)
        {
            if (UnitTestsUtil.InDevelopment)
            {
                var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
                UnitTestsUtil.AssertResult(result, system, string.Empty, unitTest);
            }
        }

    }

}
