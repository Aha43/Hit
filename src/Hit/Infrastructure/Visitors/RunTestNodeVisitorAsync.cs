using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Threading.Tasks;

namespace Hit.Infrastructure.Visitors
{
    internal class RunTestNodeVisitorAsync<World> : AbstractTestNodeVisitorAsync<World>
    {
        private readonly TestContext<World> _context;

        public RunTestNodeVisitorAsync(TestContext<World> context) => _context = context;

        public override async Task VisitAsync(TestNode<World> node, TestNode<World> parent)
        {
            var testResult = node.TestResult as TestResult;

            // Updating test context
            _context.TestName = node.TestName;
            _context.ParentTestName = node.ParentTestName;
            _context.TestResult = testResult;
            _context.Options = node.TestOptions;

            if (parent != null && parent.TestResult.Status != TestStatus.Success)
            {
                testResult.NotReached();
                return;
            }

            var test = node.Test;

            if (await TestAsync(test, _context).ConfigureAwait(false))
            { 
                testResult.Success();
            }
        }

        private async Task<bool> TestAsync(ITestLogic<World> test, ITestContext<World> context)
        {
            var ex = await PerformTestAsync(test, context).ConfigureAwait(false);
            if (ex == null) return true;
            (context.TestResult as TestResult)?.Failed(ex);
            return false;
        }

        private async Task<Exception> PerformTestAsync(ITestLogic<World> test, ITestContext<World> context)
        {
            if (test == default) return null;
            try
            {
                await test.TestAsync(context).ConfigureAwait(false);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

    }

}
