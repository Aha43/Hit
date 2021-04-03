using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TrivialSyncTestData
{
    [UseAs(test: "TrivialSyncTestB", followingTest: "TrivialSyncTestA")]
    public class TestB : TestImplementationBase<TrivialSyncTestWorld>
    {
        public override void Act(TrivialSyncTestWorld world)
        {
           
        }

    }

}
