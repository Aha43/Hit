using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestDataWithMissingParent
{
    [UseAs(test: "TestB", followingTest: "TestC")] // TestC do no exists
    public class TestB : TestImplBase<TestDataWithMissingParentWorld>
    {
    }

}
