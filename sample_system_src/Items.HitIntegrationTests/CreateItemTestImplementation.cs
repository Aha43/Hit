using Hit.Attributes;
using Hit.Infrastructure.User;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    [UseAs(test: "CreateItem")]
    public class CreateItemTestImplementation : TestImplementationBase<ItemCrudWorld>
    {
        private IItemsRepository _repository;

        public CreateItemTestImplementation(IItemsRepository repository) => _repository = repository;

        public override async Task ActAsync(ItemCrudWorld world)
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
