using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class TestBase<World> : ITest<World>
    {
        public virtual void Act(World world)
        {

        }

        public virtual async Task ActAsync(World world) => await Task.Run(() => Act(world));

    }

}
