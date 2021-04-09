using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TestData
{
    [UseAs(test: "ThisIsTestA")]
    public class TestA : TestLogicBase<TestWorld>
    {
        private ServiceForTest _service;

        public TestA(ServiceForTest service)
        {
            _service = service;
        }

        public ServiceForTest Service => _service;
    }

}
