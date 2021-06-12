using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitUnitTests
{
    public class World1 : IWorldProvider<World1>
    {
        public World1 Get() => new();
    }
    public class World2 : IWorldProvider<World2>
    {
        public World2 Get() => new();
    }
    public class World3 : IWorldProvider<World3>
    {
        public World3 Get() => new();
    }
    public class World4 : IWorldProvider<World4>
    {
        public World4 Get() => new();
    }
    public class World5 : IWorldProvider<World5>
    {
        public World5 Get() => new();
    }
    public class World6 : IWorldProvider<World6>
    {
        public World6 Get() => new();
    }
    public class World7 : IWorldProvider<World7>
    {
        public World7 Get() => new();
    }
}
