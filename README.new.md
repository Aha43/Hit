![](images/logo.png?raw=true)

[![.NET](https://github.com/Aha43/Hit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Aha43/Hit/actions/workflows/dotnet.yml)

# Hit - Hierarchically Integration Test framework

A  dotnet c# framework for integration testing where the work of one integration test can be the build up for the next integration test. Hit is intended to be used with an unit testing framework: Sequences of Hit integration tests forms unit tests. Examples here uses the XUnit framework to run the example unit tests, a similar unit test framework should also work.

* [Changelog](https://github.com/Aha43/Hit/blob/main/CHANGELOG.md)
* [NuGet Package](https://www.nuget.org/packages/Hit/)

## Getting started

### Implementing unit tests for integration testing with Hit

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
What to notice in the above example code:
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
What to notice in the above example code is:
* The implementation is used to realize three tests all following another test (use of the `UseAs` attribute's `followingTest` argument):
    * First (reading attributes from top to bottom) to follow the test that creates an item. It expects to read the created item. Test is named appropriately *ReadItemAfterCreate*
    * Second to follow a test that updates an item. It expects to read the updated item. Test is named appropriately *ReadItemAfterUpdate*.
    * Third to follow a test that deletes an item. It expects to not find the item. Test is named appropriately *ReadItemAfterDelete*. This shows how the `UseAs` attribute argument `Option` parameter can be used to alter the test logic from the default.
* The `UnitTest` parameter to the `UseAs` names a *unit test* that ends at that test, here ends at the test named *ReadItemAfterDelete* and the *unit test* is named *crud_test_run* (if is is ok to name the *unit test* the same as the last test in the sequence this can be done by giving the `UnitTest` parameter the value `'!'`).

### Configuring systems to run the unit tests

Before we can run our unit tests (sequences of Hit integration tests) we need to provide configuration for the system(s) to test. This is done by implementing the interface `ISystemConfiguration`, here is an implementation that configure the testing of an item repository that stores items in memory:

```csharp
using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Items.Infrastructure.Repository.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Items.HitIntegrationTests.Configurations
{
    [SysCon(name: "in_memory_repository_test", Description = "Testing CRUD with database repository")]
    public class InMemoryRepositoryConfiguration : SystemConfigurationAdapter<ItemCrudWorld>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            return services.ConfigureInMemoryRepositoryServices(); ;
        }

    }

}
```
What to notice in the above example code:
* The mandatory attribute `SysCon` is used to name the system configuration and an optional description may be given.
* The method `ConfigureServices` configures the services of the system to be tested.

In the very simple systems used in these examples the only configuration needed is to call an extension to `IServiceCollection` method provided by the system to configure the system's services. In real applications one will need to use configuration data (notice an IConfiguration object is accepted by the `ConfigureServices` method). See the wiki article [Configuring Systems To Run Unit Tests On](https://github.com/Aha43/Hit/wiki/Configuring-Systems-To-Run-Unit-Tests-On) for complete details on configuring system to run unit tests on.

### Running unit tests using Xunit

As stated Hit defined unit tests are intended to be run with a unit test framework, here is how it can be done using the Xunit framework: 

```csharp
using Hit.Infrastructure;
using Hit.Infrastructure.Assertions;
using Items.HitIntegrationTests;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Items.AutomaticHitIntegrationTests
{
    public class UnitTests
    {
        private static readonly UnitTestsSpace<ItemCrudWorld> _testSpace = new();

        private readonly ITestOutputHelper _testOutput;

        public UnitTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _testSpace.SetTestLogicLogger(_testOutput.WriteLine);
        }

        [Theory]
        [InlineData("rest_consuming_repository_test", "crud_test_run")]
        [InlineData("in_memory_repository_test", "crud_test_run")]
        public async Task UnitTest(string system, string unitTest)
        {
            var result = await _testSpace.RunUnitTestAsync(system, unitTest);
            result.ShouldBeenSuccessful(_testOutput.WriteLine);
        }

    }

}
```
What to notice in the above example code:
* An static instance of the type `UnitTestsSpace` is created. It uses reflection to find test implementation classes, configuration classes and read attribute information to build an internal structure of the unit tests realized so we can run them by invoking it's `RunUnitTestAsync` methods.
* Using Xunit's `Theory` and `InlineData` attributes only one Xunit test method is needed.
* We see here the multi dimensional aspect of Hit:
    * We must provide at least one unit test and one system configures to have unit tests to run. If we have one system configured the space of unit tests is one dimensional and the method `UnitTestSpace.RunUnitTest(string)` can be used to run a test.
    * If more than one system is configured (the case of the example here) the space of unit tests are two dimensional and the method `UnitTestSpace.RunUnitTest(string, string)` can be used to run a test.
    * There is also a third dimension we can add called *layer* that is out of scope of this *getting started* introduction: See the wiki article [Configuring Systems To Run Unit Tests On](https://github.com/Aha43/Hit/wiki/Configuring-Systems-To-Run-Unit-Tests-On) for details on this. If *layers* been used to create a three dimensional unit test space the method `UnitTestSpace.RunUnitTest(string, string, string)` can be used to run a test.

It must be noticed that this multi dimensional aspect of Hit poses a luxury problem: One get the potential of 

`number_of_named_unit_tests * number_of_configured_systems * number_of_layers`

number of unit tests one can run with relatively few lines of test code. This is the max number of `Inline` attributes one may have to write. The actually number may be less since there may not be a unit test at all positions (i.e a unit test do not apply for a certain layer). Now running of unit tests must be hand coded, I hope future version of Hit will provide tooling to improve the ease to cover all unit tests.

This is how all green test run then look in visual studio's test explorer: 

![](images/test-explorer-output.png?raw=true)

A nice thing with how Xunit tests show `inline` data is that each unit test's '*position*' is shown.



