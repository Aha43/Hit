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

            var result = await hit.RunTestRunAsync("TrivialSyncTestB").ConfigureAwait(false);

            var resultRoot = Assert.Single(result.Results);

            Assert.Equal("TrivialSyncTestA", resultRoot.TestResult.TestName);
            Assert.True(resultRoot.TestResult.Status == TestStatus.Success);

            var resultLeaf = resultRoot.Next;
            Assert.NotNull(resultLeaf);

            Assert.Equal("TrivialSyncTestB", resultLeaf.TestResult.TestName);
            Assert.True(resultLeaf.TestResult.Status == TestStatus.Success);
            Assert.Null(resultLeaf.Next);
        }

    }

}
