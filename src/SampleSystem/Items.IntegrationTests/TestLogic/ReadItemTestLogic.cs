﻿using Hit.Infrastructure.Attributes;
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
