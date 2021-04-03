﻿using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class TestImplementationBase<World> : ITestImplementation<World>
    {
        public virtual void Test(World world, ITestOptions options) { }
        public virtual async Task TestAsync(World world, ITestOptions options) => await Task.Run(() => Test(world, options));
    }

}
