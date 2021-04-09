using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestDataWithTestNameCollision
{
    [UseAs(test: "TestA")]
    public class TestA : TestLogicBase<TestDataWithTestNameCollisionWorld>
    {
    }

}
