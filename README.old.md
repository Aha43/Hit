![](images/logo.png?raw=true)

[![.NET](https://github.com/Aha43/Hit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Aha43/Hit/actions/workflows/dotnet.yml)

# Old README kept temporarily...

# HIT - Hierarchically Integration Test framework

**Currently being rewritten to be true to current alpha version**

A  dotnet / c# framework for integration testing where the work of one integration test can be the build up for the next integration test. HIT is intended to be used with an unit testing framework: Sequences of HIT integration tests forms unit tests. Exemples here uses the XUnit framework to run the example unit tests, a similar unit test framework should also work.

* [Changelog](https://github.com/Aha43/Hit/blob/main/CHANGELOG.md)
* [NuGet Package](https://www.nuget.org/packages/Hit/)

## Getting started

### Example of testing CRUD operations using HIT

HIT integration tests are defined by `UseAs` attributes that decorate the classes that implements the tests logic. Here is a test logic implementation that test the creating an item given a repository of items:
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
What to notice in above example code:
* The `UseAs` attribute says this test implementation is used to realize a test named *CreateItem*.
* Since a `followingTest` argument is not passed to the `UseAs` attribute (next code snippet will show use of this) the test named *CreateItem* will be the first test in a least one *unit test*.
* Dependency injection pattern can be used to inject parts of the system being tested. Here the repository implementation specified by the interface `IItemRepository` is injected. It is the case for all the [test classes](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests/TestLogic) in this example [integration test project](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests) that they get injected the repository to test like this.
* Tests communicate state through a *world* object. In this example tests read from the *world* what is to be expected before the test and write what to be expected after the test to the *world* object.
    * This example uses [ItemCrudWorld](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorld.cs) as the *world* type.
    * Test implementers must implement an `IWorldProvider` to provide *world* instances to the test framework, the sample system's integration test uses [ItemCrudWorldProvider](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorldProvider.cs)
* HIT does not provide a generally assert API, that's been done..
    * ... I like [Shouldly](https://github.com/shouldly/shouldly).
    * ... also if the HIT integration tests are implemented in the unit test project used to run the unit tests defined (see section [Using unit test frameworks to run HIT integration tests](https://github.com/Aha43/Hit/blob/main/README.md#using-unit-test-frameworks-to-run-hit-integration-tests)) you can use the assertion API the choosen unit test framework implement. 

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

See [UpdateItemTestImpl](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/TestLogic/UpdateItemTestLogic.cs) and [DeleteItemTestImpl](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/TestLogic/DeleteItemTestLogic.cs) for the complete set of test implementations in this integration test example. Examining all the `UseAs` attributes one finds that they define an unit test (tests that run in sequence independently of other unit tests) named *crud_unit_test* that
1. Create an item (test named *CreateItem*).
2. Read created item (test named *ReadItemAfterCreate*)
3. Update the item (test named *UpdateItem*)
4. Read updated item (test named *ReadItemAfterUpdate*)
5. Delete the item (test named *DeleteItem*)
6. Read the deleted item and expect not to find it (test named *ReadItemAfterDelete*)

Before we can run our unit tests (sequences of HIT integration tests) we need to provide configuration for the system(s) to test. This is done by implementing the interface `ISystemConfiguration`, here is an implementation that configure the testing of an item repository that stores items in memory:

```csharp=
[SysCon(name: "in_memory_repository_test", Description = "Testing CRUD with database repository")]
public class InMemoryRepositoryConfiguration : SystemConfigurationAdapter<ItemCrudWorld>
{
    public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        return services.ConfigureInMemoryRepositoryServices(); ;
    }

}
```


What to notice in above example code:
* The mandatory attribute `SysCon` is used to name the system configuration and a optional description may be given.
* The method `ConfigureServices` configures the services of the system to be tested.
    * The sample system tested provide an extension method to configure its services and is invoked (this will in effect 


```
Suite: in_memory_repository_test
Suite description: Testing CRUD with Items.Infrastructure.Repository.InMemory.ItemsRepository
Run name: crud_test_run
  Test: CreateItem Status: Success
    Test: ReadItemAfterCreate Status: Success
      Test: UpdateItem Status: Success
        Test: ReadItemAfterUpdate Status: Success
          Test: DeleteItem Status: Success
            Test: ReadItemAfterDelete Status: Success
```
### Testing multiple system configuration with same test setup

The sample system also provides a repository implementation that consumes the API. To also test this implementation one can of course just instantiate a second suite configured for testing the REST repository but the following code snippet shows how to use the `HitSuites` type to work with a collection of suites:
```csharp
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
```
An 'all green' output from the above look like this:
```
Suite: REST consuming repository test
Description: Testing CRUD with Items.Infrastructure.Repository.Rest.ItemsRepository
  Test: CreateItem Status: Success
    Test: ReadItemAfterCreate Status: Success
      Test: UpdateItem Status: Success
        Test: ReadItemAfterUpdate Status: Success
          Test: DeleteItem Status: Success
            Test: ReadItemAfterDelete Status: Success

Suite: In memory repository test
Description: Testing CRUD with Items.Infrastructure.Repository.InMemory.ItemsRepository
  Test: CreateItem Status: Success
    Test: ReadItemAfterCreate Status: Success
      Test: UpdateItem Status: Success
        Test: ReadItemAfterUpdate Status: Success
          Test: DeleteItem Status: Success
            Test: ReadItemAfterDelete Status: Success
```
Testing an repository consuming an API makes it easy to demonstrate output when a test fails without messing around with source code by running the tests with the service not running:
```
Suite: REST consuming repository test
Description: Testing CRUD with Items.Infrastructure.Repository.Rest.ItemsRepository
  Test: CreateItem Status: Failed
!!!
System.Net.Http.HttpRequestException: No connection could be made because the target machine actively refused it. (localhost:44356)
 ---> System.Net.Sockets.SocketException (10061): No connection could be made because the target machine actively refused it.
   at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.ThrowException(SocketError error, CancellationToken cancellationToken)
   at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.System.Threading.Tasks.Sources.IValueTaskSource.GetResult(Int16 token)
   at System.Net.Sockets.Socket.<ConnectAsync>g__WaitForConnectWithCancellation|283_0(AwaitableSocketAsyncEventArgs saea, ValueTask connectTask, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.DefaultConnectAsync(SocketsHttpConnectionContext context, CancellationToken cancellationToken)
   at System.Net.Http.ConnectHelper.ConnectAsync(Func`3 callback, DnsEndPoint endPoint, HttpRequestMessage requestMessage, CancellationToken cancellationToken)
   --- End of inner exception stack trace ---
   at System.Net.Http.ConnectHelper.ConnectAsync(Func`3 callback, DnsEndPoint endPoint, HttpRequestMessage requestMessage, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.CreateHttp11ConnectionAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.GetHttpConnectionAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpConnectionPool.SendWithRetryAsync(HttpRequestMessage request, Boolean async, Boolean doRequestAuth, CancellationToken cancellationToken)
   at System.Net.Http.RedirectHandler.SendAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.DiagnosticsHandler.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
   at System.Net.Http.HttpClient.SendAsyncCore(HttpRequestMessage request, HttpCompletionOption completionOption, Boolean async, Boolean emitTelemetryStartStop, CancellationToken cancellationToken)
   at Items.Infrastructure.Repository.Rest.ItemsRepository.PerformAsync(Object param, String method, CancellationToken cancellationToken) in D:\rep\Hit\sample_system_src\Items.Infrastructure.Repository.Rest\ItemsRepository.cs:line 34
   at Items.Infrastructure.Repository.Rest.ItemsRepository.CreateAsync(CreateItemParam param, CancellationToken cancellationToken) in D:\rep\Hit\sample_system_src\Items.Infrastructure.Repository.Rest\ItemsRepository.cs:line 24
   at Items.HitIntegrationTests.TestsImpl.CreateItemTestImpl.TestAsync(ItemCrudWorld world, ITestOptions options) in D:\rep\Hit\sample_system_src\Items.HitIntegrationTests\TestsImpl\CreateItemTestImpl.cs:line 28
   at Hit.Infrastructure.Visitors.RunTestNodeVisitorAsync`1.TestAsync(ITestImplementation`1 actor, ITestOptions options) in D:\rep\Hit\src\Hit\Infrastructure\Visitors\RunTestNodeVisitorAsync.cs:line 45
!!!
    Test: ReadItemAfterCreate Status: NotReached
      Test: UpdateItem Status: NotReached
        Test: ReadItemAfterUpdate Status: NotReached
          Test: DeleteItem Status: NotReached
            Test: ReadItemAfterDelete Status: NotReached

Suite: In memory repository test
Description: Testing CRUD with Items.Infrastructure.Repository.InMemory.ItemsRepository
  Test: CreateItem Status: Success
    Test: ReadItemAfterCreate Status: Success
      Test: UpdateItem Status: Success
        Test: ReadItemAfterUpdate Status: Success
          Test: DeleteItem Status: Success
            Test: ReadItemAfterDelete Status: Success
```
What to notice in above example output:
* Test that fail get status `Failed`.
* All tests that follows the test that failed get status `NotReached`: They are not run because an *up the river* test failed.
* Independent *test runs* are run even if a *test run* fails. 
* Exception details are of course provided in the report output.

### More important details about the framework

* The same instance of a test implementation is used by all tests defined by its `UseAs` attributes. Because of this test classes should not maintain any internal state but operate only on passed state (the *world* argument and `ITestOptions` argument).
* In the examples shown here all the test logic has tested asynchronous methods and so overrides the method `TestAsync`, to test synchronous code override the `Test` method.
* In general `UseAs` attributes can define a forest of possible complex test trees. It is important to understand that these trees defines *test runs* and not **a** *test run*. That is, one would for example be wrong if one assumed tests was executed in say a *depth first search* order. All the leaf test nodes represent a *test run* (backtracking to its root) that are run independently of each other with a world object provided separately for each *test run*.
* *Test runs* can be named by using the `TestRun` parameter of the `UseAs` attribute. If *test runs* have been named one can use the `IHitSuite` method `RunTestRunAsync` to run the named *test run*. Next section show how this can be used to take advantage the tooling around unit test frameworks for running HIT integration tests.  

### Using unit test frameworks to run HIT integration tests

Since *test runs* are run independentely they can be test in a unit test framework and so take advantage of the existing tools around unit test frameworks (continues integration, IDE test runner integration,...). The following code snippet shows testing of the CRUD operations for both repository implementations using XUnit, one test for each system configuration:
```csharp
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

    [Fact]
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
```
What to notice in above example code:
* Each unit test uses the `GetNamedSuite` to get the suite with the system configuration to test then uses the `RunTestRunAsync` to run the one *test run* (named using the `TestRun` parameter to a `UseAs` attribute) the test is about.
* While HIT does not implement yet another general assertion API it does provide an assertion method `ShouldBeenSuccessful` for use in unit test to assert the result from a HIT *test run* has been successful.

We can now for example use Visual Studio's test explorer to run the tests, again provoking a failed tests to make the result more interesting:

![](images/test-explorer-output.png?raw=true)

What to notice in above output:
* The `ShouldBeenSuccessful` assertion produces for failed test result an exception which message is produced in same way as in previous console output example.
