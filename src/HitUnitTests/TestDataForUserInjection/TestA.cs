using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestData
{
    [Test(name: "ThisIsTestA")]
    public class TestA : TestBase<TestWorld>
    {
        private ServiceForTest _service;

        public TestA(ServiceForTest service)
        {
            _service = service;
        }

        public ServiceForTest Service => _service;

        public override void Act(TestWorld world)
        {
       
        }
    }

}
