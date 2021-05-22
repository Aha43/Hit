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
    * As stated Hit is intended to be used with an unit test framework so the *host unit test framework*'s assertion API can be used.
    * Also independent assertion API exist, I like [Shouldly](https://github.com/shouldly/shouldly) used in examples here.

The next code snippet shows implementation of tests that test reading of items from repositories:
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
What to notice in above example code is:
* The implementation is used to realize three tests all following another test (use of the `UseAs` attribute's `followingTest` argument):
    * First (reading attributes from top to bottom) to follow the test that creates an item. It expects to read the created item. Test is named appropriately *ReadItemAfterCreate*
    * Second to follow a test that updates an item. It expects to read the updated item. Test is named appropriately *ReadItemAfterUpdate*.
    * Third to follow a test that deletes an item. It expects to not find the item. Test is named appropriately *ReadItemAfterDelete*. This shows how the `UseAs` attribute argument `Option` parameter can be used to alter the test logic from the default.
* The `UnitTest` parameter to the `UseAs` names a *unit test* that ends at that test, here ends at the test named *ReadItemAfterDelete* and the *unit test* is named *crud_test_run* (if is is ok to name the *unit test* the same as the last test in the sequence this can be done by giving the `UnitTest` parameter the value `'!'`).
