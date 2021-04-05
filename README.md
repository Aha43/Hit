# HIT - Hierarchically Integration Test framework

A framework for integration tests where the work of one test can be the build up for the next test.

### Example of testing CRUD operations using HIT

HIT tests are defined by `UseAs` attributes that decorate the classes that implements the tests logic. Here is a test implementation that test creating an item given a repository of items:
```csharp
[UseAs(test: "CreateItem")]
public class CreateItemTestImpl : TestImplBase<ItemCrudWorld>
{
    private readonly IItemsRepository _repository;

    public CreateItemTestImpl(IItemsRepository repository) => _repository = repository;

    public override async Task TestAsync(ItemCrudWorld world, ITestOptions options)
    {
        // arrange
        var param = new CreateItemParam
        {
            Name = "Dragon"
        };

        // act
        var created = await _repository.CreateAsync(param, CancellationToken.None).ConfigureAwait(false);

        // assert
        created.ShouldNotBe(null);
        created.Name.ShouldBe("Dragon");

        // change world state
        world.Id = created.Id;
        world.Name = created.Name;
    }

}

```
What to notice in above example code:
* The `UseAs` attribute says this test implementation is used to realize a test named *CreateItem*.
* Since a `followingTest` argument is not passed to the `UseAs` attribute (next code snippet will show use of this) the test named *CreateItem* will be the first test in a *test run*.
* Dependency injection pattern can be used to inject parts of the system being tested. Here the repository implementation specified by the interface `IItemRepository` is injected. It is the case for all the [test classes](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests/TestsImpl) in this example [integration test project](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests) that they get injected the repository to test like this.
* Tests communicate state through a *world* object. In this example tests read from the *world* what is to be expected before the test and write what to be expected after the test to the *world* object.
    * This example uses [ItemCrudWorld](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorld.cs) as the *world* type.
    * Test implementers must implement an `IWorldProvider` to provide *world* instances to the test framework, the sample system's integration test uses [ItemCrudWorldProvider](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorldProvider.cs)
* HIT does not provide a genaerally assert API, that's been done, I like [Shouldly](https://github.com/shouldly/shouldly). 

The next code snippet shows implementation of tests that test reading of items from repositories:
```csharp
[UseAs(test: "ReadItemAfterCreate", followingTest: "CreateItem")]
[UseAs(test: "ReadItemAfterUpdate", followingTest: "UpdateItem")]
[UseAs(test: "ReadItemAfterDelete", followingTest: "DeleteItem", Options = "expectToFind = false", TestRun = "CRUDTestRun")]
public class ReadItemTestImpl : TestImplBase<ItemCrudWorld>
{
    private readonly IItemsRepository _repository;

    public ReadItemTestImpl(IItemsRepository repository) => _repository = repository;

    public override async Task TestAsync(ItemCrudWorld world, ITestOptions options)
    {
        // arange
        var param = new ReadItemParam
        {
            Id = world.Id
        };

        // act
        var read = await _repository.ReadAsync(param, CancellationToken.None).ConfigureAwait(false);

        // assert
        if (options.EqualsIgnoreCase("expectToFind", "true", def: "true"))
        {
            read.ShouldNotBe(null);
            read.Id.ShouldBe(world.Id);
            read.Name.ShouldBe(world.Name);
        }
        else
        {
            read.ShouldBeNull();
        }
    }

}
```
What to notice in above example code:
* The implementation is used to realize three tests all following another test (use of the `UseAs` attribute's `followingTest` argument):
    * First (reading attributes from top to bottom) to follow the test that creates an item. It expects to read the created item. Test is named appropriately *ReadItemAfterCreate*
    * Second to follow a test that updates an item. It expects to read the updated item. Test is named appropriately *ReadItemAfterUpdate*.
    * Third to follow a test that deletes an item. It expects to not find the item. Test is named appropriately *ReadItemAfterDelete*. This shows how the `UseAs` attribute argument `Option` parameter can be used to alter the test logic from the default.

See [UpdateItemTestImpl](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/TestsImpl/UpdateItemTestImpl.cs) and [DeleteItemTestImpl](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/TestsImpl/DeleteItemTestImpl.cs) for the complete set of test implementations in this integration test example. Examining all the `UseAs` attributes one finds that they define a test run (tests that run in sequence) that
1. Create an item (test named *CreateItem*).
2. Read created item (test named *ReadItemAfterCreate*)
3. Update the item (test named *UpdateItem*)
4. Read updated item (test named *ReadItemAfterUpdate*)
5. Delete the item (test named *DeleteItem*)
6. Read the deleted item and expect not to find it (test named *ReadItemAfterDelete*)

The next code snippet show how to test a system configuration with the defined test run:
```csharp
var inMemoryRepositoryTestSuite = new HitSuite<ItemCrudWorld>(o =>
{
    o.Services.ConfigureInMemoryRepositoryServices();

    o.Name = "In memory repository test";
    o.Description = "Testing CRUD with " + typeof(Infrastructure.Repository.InMemory.ItemsRepository).FullName;
});

var result = await inMemoryRepositoryTestSuite.RunTestsAsync().ConfigureAwait(false);

var report = new ResultsReporter().Report(result);
System.Console.WriteLine(report);
```
What to notice in above example code:
* It is common for system that is using dependency injection for configuration to provide extension methods to `IServiceCollection` for registrering sub systems services, so also for the sample system: If one examine the method in [IoCConfig.cs](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.Infrastructure.Repository.InMemory/IoCConfig.cs) one will see that it registers an `Items.Infrastructure.Repository.InMemory.ItemsRepositoy` as `IItemRepository` so that is the repository implementation this suite will test.
* Suite can be given a name and a description, optional but nice to have.
* `ResultsReporter` is an utility that generates an report of test runs, output in case all tests *green*:

```
Suite: InMemoryRepository test
Description: Testing CRUD with Items.Infrastructure.Repository.InMemory.ItemsRepository
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
        var results = await suite.RunTestRunAsync("CRUDTestRun");
        results.ShouldBeenSuccessful();
    }

    [Fact]
    public async Task CrudShouldWorkForInMemoryRepositoryAsync()
    {
        var suite = _repositoryTestSuites.GetNamedSuite("In memory repository test");
        var results = await suite.RunTestRunAsync("CRUDTestRun");
        results.ShouldBeenSuccessful();
    }

}
```
What to notice in above example code:
* While HIT does not implement yet another general assertion API it does provide an assertion method `ShouldBeenSuccessful` for use in unit test to assert the result from a HIT *test run* has been successful.

We can now for example use Visual Studio's test explorer to run the tests, again provoking a failed tests to make the result more interesting:

![](images/test-explorer-output.png?raw=true)

What to notice in above output:
* The `ShouldBeenSuccessful` assertion produces for failed test result an exception which message is produced in same way as in previous console output example.
