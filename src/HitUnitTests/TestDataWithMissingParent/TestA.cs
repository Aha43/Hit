using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestDataWithMissingParent
{
    [UseAs(test: "TestA")]
    public class TestA : TestLogicBase<TestDataWithMissingParentWorld>
    {
    }

}
