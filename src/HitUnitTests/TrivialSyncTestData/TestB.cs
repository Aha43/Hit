using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TrivialSyncTestData
{
    [UseAs(test: "TrivialSyncTestB", followingTest: "TrivialSyncTestA", UnitTest = "!")]
    public class TestB : TestLogicBase<TrivialSyncTestWorld>
    {
    }

}
