using Hit.Infrastructure;
using Items.Infrastructure.Repository.InMemory;
using Items.Infrastructure.Repository.Rest;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    class Program
    {
        // The single suite example
        //static async Task Main(string[] args)
        //{
        //    var inMemoryRepositoryTestSuite = new HitSuite<ItemCrudWorld>(o => {
        //        o.Services.ConfigureInMemoryRepositoryServices();

        //        o.Name = "in_memory_repository_test";
        //        o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.InMemory.ItemsRepository).FullName;
        //    });

        //    var result = await inMemoryRepositoryTestSuite.RunTestRunAsync("crud_test_run").ConfigureAwait(false);
        //    var report = ResultsReporterUtil.Report(result);
        //    System.Console.WriteLine(report);
        //    System.Console.ReadLine();
        //}

        static async Task Main(string[] args)
        {
            var repositoryTestSuites = new HitSuites<ItemCrudWorld>()
                .AddSuite(o =>
                {
                    o.Services.ConfigureRestRepositoryServices("https://localhost:44356/");

                    o.Name = "rest_consuming_repository_test";
                    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.Rest.ItemsRepository).FullName;
                })
                .AddSuite(o =>
                {
                    o.Services.ConfigureInMemoryRepositoryServices();

                    o.Name = "in_memory_repository_test";
                    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.InMemory.ItemsRepository).FullName;
                });

            var result = await repositoryTestSuites.RunTestRunAsync("rest_consuming_repository_test", "crud_test_run").ConfigureAwait(false);
            var report = ResultsReporterUtil.Report(result);
            System.Console.WriteLine(report);

            result = await repositoryTestSuites.RunTestRunAsync("in_memory_repository_test", "crud_test_run").ConfigureAwait(false);
            report = ResultsReporterUtil.Report(result);
            System.Console.WriteLine(report);


            System.Console.ReadLine();
        }

    }

}
