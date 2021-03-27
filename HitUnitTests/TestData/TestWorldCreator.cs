using Hit.Specification.User;

namespace HitUnitTests.TestData
{
    public class TestWorldCreator : IWorldCreator<TestWorld>
    {
        public TestWorld Create()
        {
            return new TestWorld();
        }
    }
}
