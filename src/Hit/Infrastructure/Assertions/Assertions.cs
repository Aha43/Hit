using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;
using System;

namespace Hit.Infrastructure.Assertions
{
    public static class Assertions
    {
        public static void ShouldBeenSuccessful(this IUnitTestResult results, Action<string> logIfSuccessful = null)
        {
            if (!results.Success())
            {
                var report = ResultsReporterUtil.Report(results);
                throw new UnitTestFailedException(report);   
            }
            if (logIfSuccessful != null)
            {
                var report = ResultsReporterUtil.Report(results);
                logIfSuccessful.Invoke(report);
            }
        }

    }

}
