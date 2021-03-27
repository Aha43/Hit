using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class TestBase<World> : ITest<World>
    {
        private readonly IWorldActor<World> _preTestActor;
        private readonly IWorldActor<World> _postTestActor;

        protected TestBase()
        {
        }

        protected TestBase(IWorldActor<World> pre, IWorldActor<World> post)
        {
            _preTestActor = pre;
            _postTestActor = post;
        }

        public IWorldActor<World> PreTestActor => _preTestActor;

        public IWorldActor<World> PostTestActor => _postTestActor;

        public virtual void Act(World world)
        {

        }

        public virtual async Task ActAsync(World world) => await Task.Run(() => Act(world));

    }

}
