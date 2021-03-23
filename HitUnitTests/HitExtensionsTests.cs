using Hit.Infrastructure;
using HitUnitTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class HitExtensionsTests
    {
        [Fact]
        public void ShouldGetTestNameFromAtribute()
        {
            // arrange
            var type = typeof(TestA);

            // act
            var name = type.TestName<TestWorld>();

            // assert
            Assert.Equal("ThisIsTestA", name);
        }

    }

}
