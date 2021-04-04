# HIT - Hierarchically Integration Test framework

A framework for integration tests where the work of one test can be the build up for the next tests.

### Example of testing CRUD operations as defined by the interface [IItemRepository](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.Specification/IItemsRepository.cs) using HIT

HIT tests have an implementation class decorated with the `UseAs` attribute that tells how it is used to realize tests in a suite. Here is a test implementation that test creating an item given a repository for items:
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
        var created = await _repository.CreateAsync(param, CancellationToken.None);

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
* Dependency injection pattern can be used to inject part of the system tested, here the repository implmentation specified by the interface `IItemRepository`. It is the case for all the [test classes](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests/TestsImpl) in this example [integration test project](https://github.com/Aha43/Hit/tree/main/sample_system_src/Items.HitIntegrationTests) that they get injected the repository to test like this.
* Tests communicate state through a *World* object. In this example tests reads what is to be expected before the test and write to be expected after the test to the *World* object.
    * This example uses [ItemCrudWorld](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorld.cs) as the *World* type.
    * Test implementers must implement an `IWorldProvider` to provide *World* instances to the test framework, the sample system's integration test uses [ItemCrudWorldProvider](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.HitIntegrationTests/ItemCrudWorldProvider.cs)
* HIT does not provide an assert framework, thats been done, I like [Shouldly](https://github.com/shouldly/shouldly). 

The next 
