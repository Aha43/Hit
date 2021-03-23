using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure
{
    abstract class AbstractTestNodeVisitor<World>
    {
        public abstract void Visit(TestNode<World> node);
    }
}
