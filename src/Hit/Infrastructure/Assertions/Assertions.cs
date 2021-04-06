using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;

namespace Hit.Infrastructure.Assertions
{
    public static class Assertions
    {
        public static void ShouldBeenSuccessful(this IHitSuiteTestResults results)
        {
            if (!results.Success())
            {
                var rapport = new ResultsReporter().Report(results);
                throw new HitTestsFailedException(rapport);
            }
        }

    }

}
