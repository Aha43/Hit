using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using HitUnitTests.Configurations;
using HitUnitTests.Worlds;
using Xunit;

namespace HitUnitTests.TestLogic
{
    #region hierarchy1
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

    [UseAs("TestC")]
    [UseAs("TestC_1", followingTest: "TestC", Options = "fail=true")]
    [UseAs("TestC_2", followingTest: "TestC_1", UnitTest = "testC_2")]
    public class TestLogicImpl1 : UnitAsyncTestLogic<World1>
    {
    }
    #endregion

    #region hierarchy2
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

    [UseAs("TestC")]
    [UseAs("TestC_1", followingTest: "TestC", Options = "fail=true")]
    [UseAs("TestC_2", followingTest: "TestC_1", UnitTest = "testC_2")]
    public class TestLogicImpl2 : UnitSyncTestLogic<World2>
    {
    }
    #endregion

    #region hierarchy3
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

    [UseAs("TestC")]
    [UseAs("TestC_1", followingTest: "TestC", Options = "fail=true")]
    [UseAs("TestC_2", followingTest: "TestC_1", UnitTest = "testC_2")]
    public class TestLogicImpl3 : UnitAsyncTestLogic<World3>
    {
    }
    #endregion

    [UseAs("AppSettingTest1", Options = "name = configuration-no-sections-1", UnitTest = "!")]
    [UseAs("AppSettingTest2", Options = "name = configuration-no-sections-2", UnitTest = "!")]
    [UseAs("AppSettingTest3", Options = "name = configuration-sections-part-1", UnitTest = "!")]
    [UseAs("AppSettingTest4", Options = "name = configuration-sections-part-2", UnitTest = "!")]
    [UseAs("AppSettingTest5", Options = "name = configuration-user-secret", UnitTest = "!")]
    [UseAs("AppSettingTest6", Options = "name = configuration-user-secret-sections-part-1", UnitTest = "!")]
    [UseAs("AppSettingTest7", Options = "name = configuration-user-secret-sections-part-2", UnitTest = "!")]
    public class TestLogicImpl4 : TestLogicBase<World4>
    {
        private readonly ConfSetting _setting;

        public TestLogicImpl4(ConfSetting setting) => _setting = setting;

        public override void Test(ITestContext<World4> context)
        {
            Assert.NotNull(_setting);
            var expected = context.Options.Get("name");
            Assert.Equal(expected, _setting.Name);
        }

    }

    

}
