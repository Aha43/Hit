using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TrivialSyncTestData
{
    [Follows(name: "TrivialSyncTestA")]
    [Test(name: "TrivialSyncTestB")]
    public class TestB : TestBase<TrivialSyncTestWorld>
    {
        public override void Act(TrivialSyncTestWorld world)
        {
           
        }

    }

}
