using Hit.Specification.User;

namespace HitUnitTests.TestData
{
    public class TestWorldProvider : IWorldProvider<TestWorld>
    {
        public TestWorld Get()
        {
            return new TestWorld();
        }
    }
}
