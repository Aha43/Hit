using Hit.Infrastructure;
using Items.Infrastructure.Repository.InMemory;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var inMemoryRepositoryTestSuite = new HitSuite<ItemCrudWorld>(o =>
            {
                o.Services.ConfigureInMemoryRepositoryServices();

                o.Name = "InMemoryRepository test";
                o.Description = "Testing CRUD with " + typeof(Items.Infrastructure.Repository.InMemory.ItemsRepository).FullName;
            });
            
            var result = await inMemoryRepositoryTestSuite.RunTestsAsync().ConfigureAwait(false);

            var report = new ResultsReporter().Report(result);
            System.Console.WriteLine(report);

            System.Console.ReadLine();
        }

    }

}
