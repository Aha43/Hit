using Hit.Specification.User;

namespace HitUnitTests.TrivialSyncTestData
{
    public class TestWorldProvider : IWorldProvider<TrivialSyncTestWorld>
    {
        public TrivialSyncTestWorld Get()
        {
            return new TrivialSyncTestWorld();
        }
    }
}
