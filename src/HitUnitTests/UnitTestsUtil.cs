using Hit.Infrastructure.Assertions;
using Hit.Specification.Infrastructure;
using System;
using System.IO;
using Xunit;

namespace HitUnitTests
{
    public static class UnitTestsUtil
    {
        public static void AssertResult(IUnitTestResult result, string system, string layer, string unitTest, bool failed = false)
        {
            Assert.NotNull(result);
            if (failed)
            {
                Assert.False(result.Success());
            }
            else
            {
                Assert.True(result.Success());
                result.ShouldBeenSuccessful();
            }
            
            Assert.Equal(system, result.System);
            Assert.Equal(layer, result.Layer);
            Assert.Equal(unitTest, result.UnitTest);
        }

        public static bool InDevelopment
        {
            get
            {
                return File.Exists("HitDev.txt");
                //var env = Environment.GetEnvironmentVariable("HitDev");
                //if (env == null)
                //{
                //    env = Environment.GetEnvironmentVariable("HitDev", EnvironmentVariableTarget.User);
                //    if (env == null)
                //    {
                //        env = Environment.GetEnvironmentVariable("HitDev", EnvironmentVariableTarget.Process);
                //        if (env == null)
                //        {
                //            env = Environment.GetEnvironmentVariable("HitDev", EnvironmentVariableTarget.Machine);
                //        }
                //    }
                //}
                //return env != null && env.Equals("true");
            }
        }

    }

}
