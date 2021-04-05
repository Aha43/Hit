﻿using Hit.Exceptions;
using Hit.Infrastructure;
using HitUnitTests.TestDataWithMissingParent;
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

        [Fact]
        public void ShouldThrowMissingTestException()
        {
            var ex = Assert.Throws<MissingTestException>(() => new HitSuite<TestDataWithMissingParentWorld>());
            Assert.Equal("Missing test TestC referred from test TestB", ex.Message);
        }

    }

}
