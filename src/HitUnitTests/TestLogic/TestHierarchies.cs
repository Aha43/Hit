using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using HitUnitTests.Configurations;
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
    public class TestLogicImpl4 : TestLogicBase<World4>
    {
        private readonly ConfSetting1 _setting;

        public TestLogicImpl4(ConfSetting1 setting) => _setting = setting;

        public override void Test(ITestContext<World4> context)
        {
            Assert.NotNull(_setting);
            var expected = context.Options.Get("name");
            Assert.Equal(expected, _setting.Name);
        }

    }

    [UseAs("AppSettingUserSecretTest1", Options = "name = configuration-user-secret", UnitTest = "!")]
    [UseAs("AppSettingUserSecretTest2", Options = "name = configuration-user-secret-sections-part-1", UnitTest = "!")]
    [UseAs("AppSettingUserSecretTest3", Options = "name = configuration-user-secret-sections-part-2", UnitTest = "!")]
    public class TestLogicImpl5 : TestLogicBase<World5>
    {
        private readonly ConfSetting1 _setting;

        public TestLogicImpl5(ConfSetting1 setting) => _setting = setting;

        public override void Test(ITestContext<World5> context)
        {
            Assert.NotNull(_setting);
            var expected = context.Options.Get("name");
            Assert.Equal(expected, _setting.Name);
        }

    }

    [UseAs("AppSettingTest1", Options = "name1 = config1, name2 = config2", UnitTest = "!")]
    public class TestLogicImpl6 : TestLogicBase<World6>
    {
        private readonly ConfSetting1 _setting1;
        private readonly ConfSetting2 _setting2;

        public TestLogicImpl6(ConfSetting1 setting1,
            ConfSetting2 setting2)
        {
            _setting1 = setting1;
            _setting2 = setting2;
        }

        public override void Test(ITestContext<World6> context)
        {
            Assert.NotNull(_setting1);
            Assert.NotNull(_setting2);

            var expected1 = context.Options.Get("name1");
            Assert.Equal(expected1, _setting1.Name);
            var expected2 = context.Options.Get("name2");
            Assert.Equal(expected2, _setting2.Name);
        }

    }

    [UseAs("AppSettingTest1", Options = "name2 = config2, name3 = config3", UnitTest = "!")]
    public class TestLogicImpl7 : TestLogicBase<World7>
    {
        private readonly ConfSetting2 _setting2;
        private readonly ConfSetting3 _setting3;

        public TestLogicImpl7(ConfSetting2 setting2,
            ConfSetting3 setting3)
        {
            _setting2 = setting2;
            _setting3 = setting3;
        }

        public override void Test(ITestContext<World7> context)
        {
            Assert.NotNull(_setting2);
            Assert.NotNull(_setting3);

            var expected2 = context.Options.Get("name2");
            Assert.Equal(expected2, _setting2.Name);
            var expected3 = context.Options.Get("name3");
            Assert.Equal(expected3, _setting3.Name);
        }

    }

}
