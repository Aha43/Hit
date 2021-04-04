# HIT - Hierarchically Integration Test framework

A framework for integration tests where the work of one test can be the build up for the next tests.

### Example of testing CRUD operations as defined by the interface [IItemRepository](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.Specification/IItemsRepository.cs) using HIT

HIT tests have an implementation class decorated with the `UseAs` attribute that tells how it is used to realize tests in a suite. Here is a test implementation that test creating an item given a repository for items:
```csharp
[UseAs(test: "CreateItem")]
public class CreateItemTestImpl : TestImplementationBase<ItemCrudWorld>
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
* The UseAs attribute says this test implementation is used to realize a test named CreateItem.
* Since a followsTest argument is not passed to the UseAs attribute (next code snippet will show use of this) the test named CreateItem will be the first test in a *test run*.
* Dependency injection pattern can be used to inject part of the system tested, here the repository implmentation.
* Tests communicate state through a *World* object. In the example we are going through here tests reads what is to be expected before the test and write to be expected after the test to the *World* object.
* HIT does not provide an assert framework, thats been done, I like flowly and it is used in the example code. 
