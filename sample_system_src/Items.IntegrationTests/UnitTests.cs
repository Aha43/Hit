using Hit.Infrastructure;
using Hit.Infrastructure.Assertions;
using Items.HitIntegrationTests;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Items.AutomaticHitIntegrationTests
{
    public class UnitTests
    {
        private static readonly UnitTestsSpace<ItemCrudWorld> _testSpace = new();

        private readonly ITestOutputHelper _testOutput;

        public UnitTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _testSpace.SetTestLogicLogger(_testOutput.WriteLine);
        }

        [Theory]
        [InlineData("rest_consuming_repository_test", "crud_test_run")]
        [InlineData("in_memory_repository_test", "crud_test_run")]
        public async Task UnitTest(string system, string unitTest)
        {
            var result = await _testSpace.RunUnitTestAsync(system, unitTest);
            result.ShouldBeenSuccessful(_testOutput.WriteLine);
        }

    }

}
