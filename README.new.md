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
using Hit.Infrastructure.User.Utility;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestLogic
{
    [UseAs(test: "CreateItem")]
    public class CreateItemTestLogic : TestLogicBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        private readonly TestLogMessageBuilder<ItemCrudWorld> _msgBuilder;

        public CreateItemTestLogic(IItemsRepository repository)
        {
            _repository = repository;

            _msgBuilder = new TestLogMessageBuilder<ItemCrudWorld>(this)
                .UsingService(typeof(IItemsRepository), repository);
        }

        public override async Task TestAsync(ITestContext<ItemCrudWorld> testContext)
        {
            _msgBuilder.ClearTransient().InContext(testContext);

            var param = new CreateItemParam
            {
                Name = "Dragon"
            };

            var created = await _repository.CreateAsync(param, CancellationToken.None).ConfigureAwait(false);

            created.ShouldNotBe(null);
            created.Name.ShouldBe("Dragon");

            testContext.World.Id = created.Id;
            testContext.World.Name = created.Name;

            testContext.Log(_msgBuilder.ToString());
        }

    }

}
```
What to notice in above example code:
* The `UseAs` attribute says this test implementation is used to realize a test named *CreateItem*.
* Since a `followingTest` argument is not passed to the `UseAs` attribute (next code snippet will show use of this) the test named *CreateItem* will be the first test in a least one *unit test*.
* Dependency injection pattern can be used to inject parts of the system being tested. Here the repository implementation specified by the interface `IItemRepository` is injected. It is the case for all the [test classes](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests/TestLogic) in this example [integration test project](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests) that they get injected the repository to test like this.
* Tests communicate state through a *world* object. In this example tests read from the *world* what is to be expected before the test and write what to be expected after the test to the *world* object.
    * This example uses [ItemCrudWorld](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorld.cs) as the *world* type.
    * Test implementers must implement an `IWorldProvider` to provide *world* instances to the test framework, the sample system's integration test uses [ItemCrudWorldProvider](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorldProvider.cs)
* Hit does not provide a generally assertion API, that's been done:
    * As stated Hit is intended to be used with an Unit test framework so the *host unit test framework*'s assertion API can be used.
    * Also independent assertion API exist, I like [Shouldly](https://github.com/shouldly/shouldly) used in examples here.
