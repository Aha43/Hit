using System;

namespace Hit.Infrastructure.Exceptions
{
    public class UnitTestNotFoundException : Exception
    {
        public UnitTestNotFoundException(string system, string layer, string unitTest)
            : base($"({(!string.IsNullOrWhiteSpace(system) ? "system: " + system + ", " : "")}{(!string.IsNullOrWhiteSpace(layer) ? "layer: " + layer + ", " : "")}unitTest: { unitTest}")
        {
        }
    }
}
