using Hit.Infrastructure;
using HitUnitTests.TestData;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HitUnitTests
{
    public class UserIoCTests
    {
        [Fact]
        public void UserServiceShouldBeenInjected()
        {
            
            // Arrange & act
            //var hit = new HitSuite<TestWorld>(o =>
            //{
            //    o.Services.AddSingleton(new ServiceForTest());
            //});

            //// Assert
            //Assert.NotNull((hit.GetTest("ThisIsTestA") as TestA)?.Service);
        }

    }

}
