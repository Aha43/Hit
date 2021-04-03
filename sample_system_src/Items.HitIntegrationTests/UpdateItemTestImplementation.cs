using Hit.Attributes;
using Hit.Infrastructure.User;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    [UseAs(test: "UpdateItem", followingTest: "ReadItemAfterCreate")]
    public class UpdateItemTestImplementation : TestImplementationBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public UpdateItemTestImplementation(IItemsRepository repository) => _repository = repository;

        public override async Task ActAsync(ItemCrudWorld world)
        {
            // arrange
            var param = new UpdateItemParam
            {
                Id = world.Id,
                Name = "DragonFly"
            };

            // act
            var updated = await _repository.UpdateAsync(param, CancellationToken.None);

            // assert
            updated.ShouldNotBe(null);
            updated.Id.ShouldBe(world.Id);
            updated.Name.ShouldBe("DragonFly");

            // change world state
            world.Name = "DragonFly";
        }

    }
}
