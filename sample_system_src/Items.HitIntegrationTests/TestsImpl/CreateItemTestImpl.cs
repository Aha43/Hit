using Hit.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestsImpl
{
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

}
