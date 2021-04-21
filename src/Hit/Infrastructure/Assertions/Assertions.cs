using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;
using System;

namespace Hit.Infrastructure.Assertions
{
    public static class Assertions
    {
        public static void ShouldBeenSuccessful(this IUnitTestResult results, Action<string> log = null)
        {
            if (!results.Success() && results.SystemAvailable)
            {
                var report = ResultsReporterUtil.Report(results);   
                throw new UnitTestFailedException(report);   
            }
            if (log != null)
            {
                var report = ResultsReporterUtil.Report(results);
                log.Invoke(report);
            }
        }

    }

}
