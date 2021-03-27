using Hit.Infrastructure;
using HitUnitTests.TestData;
using System;
using Xunit;

namespace HitUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var hit = new HitSuite<TestWorld>();
            hit.Initialize();

            var result = hit.RunTests();
        }

    }

}
