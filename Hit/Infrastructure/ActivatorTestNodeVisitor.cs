using Hit.Exceptions;
using Hit.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure
{
    internal class ActivatorTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        public readonly IServiceProvider _serviceProvider;

        internal ActivatorTestNodeVisitor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override void Visit(TestNode<World> node)
        {
            try
            {
                node.Test = _serviceProvider.GetService(node.TestType) as ITest<World>;
            }
            catch (Exception ex)
            {
                throw new FailedToCreateTestInstanceException(node.TestType, ex);
            }

            if (node.TestType == null)
            {
                throw new FailedToCreateTestInstanceException(node.TestType);
            }
        }

    }

}
