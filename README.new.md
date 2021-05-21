![](images/logo.png?raw=true)

[![.NET](https://github.com/Aha43/Hit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Aha43/Hit/actions/workflows/dotnet.yml)

# Hit - Hierarchically Integration Test framework

A  dotnet c# framework for integration testing where the work of one integration test can be the build up for the next integration test. Hit is intended to be used with an unit testing framework: Sequences of Hit integration tests forms unit tests. Examples here uses the XUnit framework to run the example unit tests, a similar unit test framework should also work.

* [Changelog](https://github.com/Aha43/Hit/blob/main/CHANGELOG.md)
* [NuGet Package](https://www.nuget.org/packages/Hit/)

## Getting started

### Example of testing CRUD operations using Hit

Hit integration tests are defined by `UseAs` attributes that decorate the classes that implements the tests logic. Here is a test logic implementation that test the creating an item given a repository of items:
```csharp
using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestLogic
{
    [UseAs(test: "ReadItemAfterCreate", followingTest: "CreateItem")]
    [UseAs(test: "ReadItemAfterUpdate", followingTest: "UpdateItem")]
    [UseAs(test: "ReadItemAfterDelete", followingTest: "DeleteItem", Options = "expectToFind = false", UnitTest = "crud_test_run")]
    public class ReadItemTestLogic : TestLogicBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public ReadItemTestLogic(IItemsRepository repository) => _repository = repository;

        public override async Task TestAsync(ITestContext<ItemCrudWorld> testContext)
        {
            var param = new ReadItemParam
            {
                Id = testContext.World.Id
            };

            var read = await _repository.ReadAsync(param, CancellationToken.None).ConfigureAwait(false);

            if (testContext.Options.GetAsBoolean("expectToFind", true))
            {
                read.ShouldNotBe(null);
                read.Id.ShouldBe(testContext.World.Id);
                read.Name.ShouldBe(testContext.World.Name);
                return;   
            }

            read.ShouldBeNull();
        }

    }

}
```
