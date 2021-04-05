using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestDataWithMissingParent
{
    [UseAs(test: "TestA")]
    public class TestA : TestImplBase<TestDataWithMissingParentWorld>
    {
    }

}
