using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestDataWithTestNameCollision
{
    [UseAs(test: "TestA")]
    public class TestA : TestImplBase<TestDataWithTestNameCollisionWorld>
    {
    }

}
