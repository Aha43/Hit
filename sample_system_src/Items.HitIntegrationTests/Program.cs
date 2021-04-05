using Hit.Infrastructure;
using Items.Infrastructure.Repository.InMemory;
using Items.Infrastructure.Repository.Rest;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var repositoryTestSuites = new HitSuites<ItemCrudWorld>()
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

            var result = await repositoryTestSuites.RunTestsAsync().ConfigureAwait(false);

            var report = new ResultsReporter().Report(result);
            System.Console.WriteLine(report);
            System.Console.ReadLine();
        }

    }

}
