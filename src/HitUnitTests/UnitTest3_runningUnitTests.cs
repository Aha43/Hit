using Hit.Infrastructure;
using Hit.Specification.Infrastructure;
using HitUnitTests.Assertions;
using HitUnitTests.Worlds;
using System.Threading.Tasks;
using Xunit;

namespace HitUnitTests
{
    public class UnitTest3_runningSyncUnitTests
    {
        private static IUnitTestsSpace<World3> _unitTestsSpace = new UnitTestsSpace<World3>();

        [Theory]
        [InlineData("System3_1", "Layer2", "testA_3")]
        [InlineData("System3_1", "Layer2", "testA_1_1")]
        [InlineData("System3_1", "Layer2", "testA_3_1")]
        
        [InlineData("System3_1", "Layer3", "testA_3")]
        [InlineData("System3_1", "Layer3", "testA_1_1")]
        [InlineData("System3_1", "Layer3", "testA_3_1")]
        
        [InlineData("System3_2", "Layer1", "testA_3")]
        [InlineData("System3_2", "Layer1", "testA_1_1")]
        [InlineData("System3_2", "Layer1", "testA_3_1")]
        public async Task UnitTestShouldNotFailAsync(string system, string layer, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, unitTest);
            Assert.NotNull(result);
            Assert.True(result.Success());
            Assert.Equal(unitTest, result.UnitTest);
            Assert.Equal(system, result.System);
            Assert.Equal(layer, result.Layer);
        }

        [Theory]
        [InlineData("System3_1", "Layer2", "testB_2")]
        [InlineData("System3_1", "Layer2", "testC_2")]
        
        [InlineData("System3_1", "Layer3", "testB_2")]
        [InlineData("System3_1", "Layer3", "testC_2")]
        
        [InlineData("System3_2", "Layer1", "testB_2")]
        [InlineData("System3_2", "Layer1", "testC_2")]
        public async Task UnitTestShouldFailAsync(string system, string layer, string unitTest)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, unitTest);
            Assert.NotNull(result);
            Assert.False(result.Success());
            Assert.Equal(unitTest, result.UnitTest);
            Assert.Equal(system, result.System);
            Assert.Equal(layer, result.Layer);
        }

        [Theory]
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_3_Async(string system, string layer)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, "testA_3");
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
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_1_1_Async(string system, string layer)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, "testA_1_1");
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
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public async Task SuccessResultStructureShouldBeAsExpected_testA_3_1_Async(string system, string layer)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, "testA_3_1");
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
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public async Task SuccessResultStructureShouldBeAsExpected_testB_2_Async(string system, string layer)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, "testB_2");
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
        [InlineData("System3_1", "Layer2")]
        [InlineData("System3_1", "Layer3")]
        [InlineData("System3_2", "Layer1")]
        public async Task SuccessResultStructureShouldBeAsExpected_testC_2_Async(string system, string layer)
        {
            var result = await _unitTestsSpace.RunUnitTestAsync(system, layer, "testC_2");
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
