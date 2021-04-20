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
        private readonly UnitTestsSet<ItemCrudWorld> _repositoriesUnitTests;

        private readonly ITestOutputHelper _testOutput;

        public CrudTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _repositoriesUnitTests = new UnitTestsSet<ItemCrudWorld>(); 
        }

#pragma warning disable xUnit1004 // Test methods should not be skipped
        //[Fact(Skip = "Need api service running, activate for demo")]
#pragma warning restore xUnit1004 // Test methods should not be skipped
        [Fact]
        public async Task CrudShouldWorkForRestRepositoryAsync()
        {
            var unitTests = _repositoriesUnitTests.GetNamedUnitTests("rest_consuming_repository_test");
            var results = await unitTests.RunUnitTestAsync("crud_test_run").ConfigureAwait(false);
            results.ShouldBeenSuccessful(_testOutput.WriteLine);
        }

        [Fact]
        public async Task CrudShouldWorkForInMemoryRepositoryAsync()
        {
            var unitTests = _repositoriesUnitTests.GetNamedUnitTests("in_memory_repository_test");
            var results = await unitTests.RunUnitTestAsync("crud_test_run").ConfigureAwait(false);
            results.ShouldBeenSuccessful(_testOutput.WriteLine);
        }

    }

}
