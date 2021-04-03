using Hit.Attributes;
using Hit.Infrastructure.User;
using Items.Domain.Param;
using Items.Specification;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;

namespace Items.HitIntegrationTests
{
    [Test(name: "CreateItem")]
    public class CreateItemTest : TestBase<ItemCrudWorld>
    {
        private IItemsRepository _repository;

        public CreateItemTest(IItemsRepository repository)
        {
            _repository = repository;
        }

        public override async Task ActAsync(ItemCrudWorld world)
        {
            // arrange
            var param = new CreateItemParam
            {
                Name = "Dragon"
            };

            // act
            var created = await _repository.CreateAsync(param, CancellationToken.None);

            created.ShouldNotBe(null);
            created.Name.ShouldBe("Dragon");

            // change
            world.Id = created.Id;
            world.Name = created.Name;
        }

    }

}
