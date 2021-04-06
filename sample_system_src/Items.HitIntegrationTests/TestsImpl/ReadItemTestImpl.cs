﻿using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestsImpl
{
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

}
