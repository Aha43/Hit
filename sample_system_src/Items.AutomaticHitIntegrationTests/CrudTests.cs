using Hit.Infrastructure;
using Hit.Infrastructure.Assertions;
using Items.HitIntegrationTests;
using Items.Infrastructure.Repository.InMemory;
using Items.Infrastructure.Repository.Rest;
using System.Threading.Tasks;
using Xunit;

namespace Items.AutomaticHitIntegrationTests
{
    public class CrudTests
    {
        private readonly HitSuites<ItemCrudWorld> _repositoryTestSuites;

        public CrudTests()
        {
            _repositoryTestSuites = new HitSuites<ItemCrudWorld>()
                .AddSuite(o =>
                {
                    o.Services.ConfigureRestRepositoryServices("https://localhost:44356/");
                    o.EnvironmentType = "test_env";
                    o.Name = "rest_consuming_repository_test";
                    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.Rest.ItemsRepository).FullName;
                })
                .AddSuite(o =>
                {
                    o.Services.ConfigureInMemoryRepositoryServices();
                    o.EnvironmentType = "test_env";
                    o.Name = "in_memory_repository_test";
                    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.InMemory.ItemsRepository).FullName;
                });
        }

#pragma warning disable xUnit1004 // Test methods should not be skipped
        [Fact(Skip = "Need api service running, activate for demo")]
#pragma warning restore xUnit1004 // Test methods should not be skipped
        public async Task CrudShouldWorkForRestRepositoryAsync()
        {
            var suite = _repositoryTestSuites.GetNamedSuite("rest_consuming_repository_test");
            var results = await suite.RunTestRunAsync("crud_test_run").ConfigureAwait(false);
            results.ShouldBeenSuccessful();
        }

        [Fact]
        public async Task CrudShouldWorkForInMemoryRepositoryAsync()
        {
            var suite = _repositoryTestSuites.GetNamedSuite("in_memory_repository_test");
            var results = await suite.RunTestRunAsync("crud_test_run").ConfigureAwait(false);
            results.ShouldBeenSuccessful();
        }

    }

}
