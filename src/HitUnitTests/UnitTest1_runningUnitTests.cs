using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Assertions;
using HitUnitTests.Worlds;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTest1_runningUnitTests
    {
        private static readonly IUnitTestsSpace<World1> _unitTestsSpace = new UnitTestsSpace<World1>();

        [Theory]
        [InlineData("testA_3")]
        [InlineData("testA_1_1")]
        [InlineData("testA_3_1")]
        public async Task UnitTestShouldNotFailAsync(string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(unitTest);

            UnitTestsUtil.AssertResult(result, "System1", string.Empty, unitTest);
        }

        [Theory]
        [InlineData("testB_2")]
        [InlineData("testC_2")]
        public async Task UnitTestShouldFailAsync(string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(unitTest);

            UnitTestsUtil.AssertResult(result, "System1", string.Empty, unitTest, true);
        }

        [Fact]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_3_Async()
        {
            var result = await _unitTestsSpace.RunUnitTestAsync("testA_3");

            UnitTestsUtil.AssertResult(result, "System1", string.Empty, "testA_3");

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

        [Fact]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_1_1_Async()
        {
            var result = await _unitTestsSpace.RunUnitTestAsync("testA_1_1");
            
            UnitTestsUtil.AssertResult(result, "System1", string.Empty, "testA_1_1");
            
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

        [Fact]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_3_1_Async()
        {
            var result = await _unitTestsSpace.RunUnitTestAsync("testA_3_1");
            
            UnitTestsUtil.AssertResult(result, "System1", string.Empty, "testA_3_1");
            
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

        [Fact]
        public async Task FailedResultStructureShouldBeAsExpected_testB_2_Async()
        {
            var result = await _unitTestsSpace.RunUnitTestAsync("testB_2");
            
            UnitTestsUtil.AssertResult(result, "System1", string.Empty, "testB_2", true);
            
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

        [Fact]
        public async Task FailedResultStructureShouldBeAsExpected_testC_2_Async()
        {
            var result = await _unitTestsSpace.RunUnitTestAsync("testC_2");
            
            UnitTestsUtil.AssertResult(result, "System1", string.Empty, "testC_2", true);
            
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
