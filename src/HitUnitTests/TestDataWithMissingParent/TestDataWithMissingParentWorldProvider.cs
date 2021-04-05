using Hit.Specification.User;

namespace HitUnitTests.TestDataWithMissingParent
{
    public class TestDataWithMissingParentWorldProvider : IWorldProvider<TestDataWithMissingParentWorld>
    {
        public TestDataWithMissingParentWorld Get()
        {
            return new TestDataWithMissingParentWorld();
        }
    }
}
