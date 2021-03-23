using Hit.Attributes;
using Hit.Specification;

namespace HitUnitTests.TestData
{
    [Test(name: "ThisIsTestA")]
    public class TestA : ITest<TestWorld>
    {
        public IWorldActor<TestWorld> PreTestActor => default;

        public IWorldActor<TestWorld> PostTestActor => default;

        public void Act(TestWorld world)
        {
       
        }
    }

}
