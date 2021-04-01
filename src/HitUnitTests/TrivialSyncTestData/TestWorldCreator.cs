using Hit.Specification.User;

namespace HitUnitTests.TrivialSyncTestData
{
    public class TestWorldCreator : IWorldCreator<TrivialSyncTestWorld>
    {
        public TrivialSyncTestWorld Create()
        {
            return new TrivialSyncTestWorld();
        }
    }
}
