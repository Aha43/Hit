using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using System;

namespace HitUnitTests.TestLogic
{
    public abstract class UnitSyncTestLogic<World> : TestLogicBase<World>
    {
        public override void Test(ITestContext<World> context)
        {
            if (context.Options.GetAsBoolean("fail"))
            {
                throw new Exception("Simulated test failure");
            }
        }

    }

}
