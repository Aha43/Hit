using Hit.Infrastructure.Attributes;
using HitUnitTests.TestLogic;
using HitUnitTests.Worlds;

namespace HitUnitTests
{
    [UseAs("TestA")]
    
    [UseAs("TestA_1", followingTest: "TestA")]
    [UseAs("TestA_2", followingTest: "TestA")]
    [UseAs("TestA_3", followingTest: "TestA", UnitTest = "testA_3")]

    [UseAs("TestA_1_1", followingTest: "TestA_1", UnitTest = "testA_1_1")]
    [UseAs("TestA_1_2", followingTest: "TestA_1")]

    [UseAs("TestA_3_1", followingTest: "TestA_3", UnitTest = "testA_3_1")]

    [UseAs("TestB", Options = "fail=true")]
    [UseAs("TestB_1", followingTest: "TestB")]
    [UseAs("TestB_2", followingTest: "TestB_1", UnitTest = "testB_2")]
    public class TestLogicImpl : UnitAsyncTestLogic<World1>
    {
    }
}
