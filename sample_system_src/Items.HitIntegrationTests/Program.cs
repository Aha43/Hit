using Hit.Infrastructure;
using Items.Infrastructure.Repository.InMemory;
using System;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var testInMemoryRepositorySuite = new HitSuite<ItemCrudWorld>(o =>
            {
                o.Services.ConfigureInMemoryRepositoryServices();
            });
            var result = await testInMemoryRepositorySuite.RunTestsAsync();

        }

    }

}
