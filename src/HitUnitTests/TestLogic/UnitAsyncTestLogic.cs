using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using System;
using System.Threading.Tasks;

namespace HitUnitTests.TestLogic
{
    public abstract class UnitAsyncTestLogic<World> : TestLogicBase<World>
    {
        public override async Task TestAsync(ITestContext<World> context)
        {
            await Task.Run(() => 
            {
                if (context.Options.GetAsBoolean("fail"))
                {
                    throw new Exception("Simulated test failure");
                }
            }).ConfigureAwait(false);
        }

    }

}
