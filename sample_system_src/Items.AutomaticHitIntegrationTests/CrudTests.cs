using Hit.Infrastructure;
using Hit.Infrastructure.Assertions;
using Items.HitIntegrationTests;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Items.AutomaticHitIntegrationTests
{
    public class CrudTests
    {
        private static readonly UnitTestsSpace<ItemCrudWorld> _testSpace = new();

        private readonly ITestOutputHelper _testOutput;

        public CrudTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _testSpace.SetTestLogicLogger(_testOutput.WriteLine);
        }

        [Fact]
        public async Task CrudShouldWorkForRestRepositoryAsync()
        {
            var unitTests = _testSpace.GetUnitTests("rest_consuming_repository_test");
            var results = await unitTests.RunUnitTestAsync("crud_test_run").ConfigureAwait(false);
            results.ShouldBeenSuccessful(_testOutput.WriteLine);
        }

        [Fact]
        public async Task CrudShouldWorkForInMemoryRepositoryAsync()
        {
            var unitTests = _testSpace.GetUnitTests("in_memory_repository_test");
            var results = await unitTests.RunUnitTestAsync("crud_test_run").ConfigureAwait(false);
            results.ShouldBeenSuccessful(_testOutput.WriteLine);
        }

    }

}
