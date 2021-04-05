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

                    o.Name = "REST consuming repository test";
                    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.Rest.ItemsRepository).FullName;
                })
                .AddSuite(o =>
                {
                    o.Services.ConfigureInMemoryRepositoryServices();

                    o.Name = "In memory repository test";
                    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.InMemory.ItemsRepository).FullName;
                });
        }

        [Fact]
        public async Task CrudShouldWorkForRestRepositoryAsync()
        {
            var suite = _repositoryTestSuites.GetNamedSuite("REST consuming repository test");
            var results = await suite.RunTestRunAsync("CRUDTestRun").ConfigureAwait(false);
            results.ShouldBeenSuccessful();
        }

        [Fact]
        public async Task CrudShouldWorkForInMemoryRepositoryAsync()
        {
            var suite = _repositoryTestSuites.GetNamedSuite("In memory repository test");
            var results = await suite.RunTestRunAsync("CRUDTestRun").ConfigureAwait(false);
            results.ShouldBeenSuccessful();
        }

    }

}
