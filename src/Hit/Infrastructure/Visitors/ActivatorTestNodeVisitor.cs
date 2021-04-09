using Hit.Infrastructure.Exceptions;
using Hit.Specification.User;
using System;

namespace Hit.Infrastructure.Visitors
{
    internal class ActivatorTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        public readonly IServiceProvider _serviceProvider;

        internal ActivatorTestNodeVisitor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override void Visit(TestNode<World> node, TestNode<World> parent)
        {
            try
            {
                node.Test = _serviceProvider.GetService(node.TestImplementationType) as ITestLogic<World>;
            }
            catch (Exception ex)
            {
                throw new FailedToCreateTestInstanceException(node.TestImplementationType, ex);
            }

            if (node.Test == null)
            {
                throw new FailedToCreateTestInstanceException(node.TestImplementationType);
            }
        }

    }

}
