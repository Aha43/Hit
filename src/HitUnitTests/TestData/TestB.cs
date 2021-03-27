using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestData
{
    [Parent(name: "ThisIsTestA")]
    [Test(name: "ThisIsTestB")]
    public class TestB : TestBase<TestWorld>
    {
        public override void Act(TestWorld world)
        {
           
        }

    }

}
