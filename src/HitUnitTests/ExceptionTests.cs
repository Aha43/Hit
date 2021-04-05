using Hit.Exceptions;
using Hit.Infrastructure;
using HitUnitTests.TestDataWithTestNameCollision;
using Xunit;

namespace HitUnitTests
{
    public class ExceptionTests
    {
        [Fact]
        public void ShouldThrowNameCollisionException()
        {
            var ex = Assert.Throws<TestNameCollisionException>(() => new HitSuite<TestDataWithTestNameCollisionWorld>());
            Assert.Equal("TestA", ex.Message);
        }

    }

}
