using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TrivialSyncTestData
{
    [UseAs(test: "TrivialSyncTestA")]
    public class TestA : TestLogicBase<TrivialSyncTestWorld>
    {
    }

}
