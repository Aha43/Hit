using Hit.Infrastructure;
using HitUnitTests.TestData;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var hit = new HitSuite<TestWorld>();

            var result = await hit.RunTestsAsync();
        }

    }

}
