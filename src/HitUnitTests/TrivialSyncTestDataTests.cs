using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.TrivialSyncTestData;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class TrivialSyncTestDataTests
    {
        [Fact]
        public async Task AllTwoTestShouldAsync()
        {
            var hit = new HitSuite<TrivialSyncTestWorld>();

            var result = await hit.RunTestsAsync();

            var resultRoot = Assert.Single(result);
            
            Assert.Equal("TrivialSyncTestA", resultRoot.TestResult.TestName);
            Assert.True(resultRoot.TestResult.Status == TestStatus.Success);

            var resultLeaf = Assert.Single(resultRoot.Children);

            Assert.Equal("TrivialSyncTestB", resultLeaf.TestResult.TestName);
            Assert.True(resultLeaf.TestResult.Status == TestStatus.Success);
            Assert.Empty(resultLeaf.Children);
        }

    }

}
