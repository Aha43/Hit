using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TrivialSyncTestData
{
    [UseAs(test: "TrivialSyncTestB", followingTest: "TrivialSyncTestA", TestRun = "!")]
    public class TestB : TestLogicBase<TrivialSyncTestWorld>
    {
    }

}
