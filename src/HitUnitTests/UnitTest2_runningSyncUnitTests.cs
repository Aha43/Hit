using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Assertions;
using HitUnitTests.Worlds;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTest2_runningSyncUnitTests
    {
        private static readonly IUnitTestsSpace<World2> _unitTestsSpace = new UnitTestsSpace<World2>();

        [Theory]
        [InlineData("System2_1", "testA_3")]
        [InlineData("System2_1", "testA_1_1")]
        [InlineData("System2_1", "testA_3_1")]
        [InlineData("System2_2", "testA_3")]
        [InlineData("System2_2", "testA_1_1")]
        [InlineData("System2_2", "testA_3_1")]
        public async Task UnitTestShouldNotFailAsync(string system, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
            Assert.NotNull(result);
            Assert.True(result.Success());
            Assert.Equal(unitTest, result.UnitTest);
            Assert.Equal(system, result.System);
            Assert.Equal(string.Empty, result.Layer);
        }

        [Theory]
        [InlineData("System2_1", "testB_2")]
        [InlineData("System2_1", "testC_2")]
        [InlineData("System2_2", "testB_2")]
        [InlineData("System2_2", "testC_2")]
        public async Task UnitTestShouldFailAsync(string system, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, unitTest);
            Assert.NotNull(result);
            Assert.False(result.Success());
            Assert.Equal(unitTest, result.UnitTest);
            Assert.Equal(system, result.System);
            Assert.Equal(string.Empty, result.Layer);
        }

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_3_Async(string system)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, "testA_3");
            result.ResultHead.NodeGotTestResult(result => 
            {
                result.IsForTest("TestA")
                    .HasStatus(TestStatus.Success);
            })
            .GotNextNode().NodeGotTestResult(result => 
            {
                result.IsForTest("TestA_3")
                    .HasStatus(TestStatus.Success);
            })
            .NoNextNode();
        }

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_1_1_Async(string system)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, "testA_1_1");
            result.ResultHead.NodeGotTestResult(result =>
            {
                result.IsForTest("TestA")
                    .HasStatus(TestStatus.Success);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestA_1")
                    .HasStatus(TestStatus.Success);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestA_1_1")
                    .HasStatus(TestStatus.Success);
            })
            .NoNextNode();
        }

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_3_1_Async(string system)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, "testA_3_1");
            result.ResultHead.NodeGotTestResult(result =>
            {
                result.IsForTest("TestA")
                    .HasStatus(TestStatus.Success);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestA_3")
                    .HasStatus(TestStatus.Success);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestA_3_1")
                    .HasStatus(TestStatus.Success);
            })
            .NoNextNode();
        }

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public async Task SuccessResultStructureShouldBeAsExpected_testB_2_Async(string system)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, "testB_2");
            result.ResultHead.NodeGotTestResult(result =>
            {
                result.IsForTest("TestB")
                    .HasStatus(TestStatus.Failed);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestB_1")
                    .HasStatus(TestStatus.NotReached);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestB_2")
                    .HasStatus(TestStatus.NotReached);
            })
            .NoNextNode();
        }

        [Theory]
        [InlineData("System2_1")]
        [InlineData("System2_2")]
        public async Task SuccessResultStructureShouldBeAsExpected_testC_2_Async(string system)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, "testC_2");
            result.ResultHead.NodeGotTestResult(result =>
            {
                result.IsForTest("TestC")
                    .HasStatus(TestStatus.Success);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestC_1")
                    .HasStatus(TestStatus.Failed);
            })
            .GotNextNode().NodeGotTestResult(result =>
            {
                result.IsForTest("TestC_2")
                    .HasStatus(TestStatus.NotReached);
            })
            .NoNextNode();
        }

    }

}
