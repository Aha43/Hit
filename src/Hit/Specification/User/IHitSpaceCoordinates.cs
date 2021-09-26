using System.Collections.Generic;

namespace Hit.Specification.User
{
    public interface IHitSpaceCoordinates
    {
        IEnumerable<(string system, string layer, string unitTests)> UnitTestCoordinates { get; }
    }
}
