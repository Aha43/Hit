using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;

namespace Hit.Infrastructure.Assertions
{
    public static class Assertions
    {
        public static void ShouldBeenSuccessful(this ITestRunResult results)
        {
            if (!results.Success())
            {
                var rapport = ResultsReporterUtil.Report(results);
                throw new HitTestsFailedException(rapport);
            }
        }

    }

}
