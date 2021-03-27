using Hit.Specification.User;

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

        public abstract void Act(World world);
    }

}
