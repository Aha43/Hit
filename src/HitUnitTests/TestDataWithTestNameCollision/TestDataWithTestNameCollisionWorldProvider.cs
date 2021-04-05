using Hit.Specification.User;

namespace HitUnitTests.TestDataWithTestNameCollision
{
    public class TestDataWithTestNameCollisionWorldProvider : IWorldProvider<TestDataWithTestNameCollisionWorld>
    {
        public TestDataWithTestNameCollisionWorld Get()
        {
            return new TestDataWithTestNameCollisionWorld();
        }
    }
}
