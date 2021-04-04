using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IResultsReporter
    {
        string Report(IHitSuiteTestResults results);
        string Report(IEnumerable<IHitSuiteTestResults> results);
    }
}
