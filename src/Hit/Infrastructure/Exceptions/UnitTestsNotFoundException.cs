using System;

namespace Hit.Infrastructure.Exceptions
{
    public class UnitTestsNotFoundException : Exception
    {
        public UnitTestsNotFoundException(string system, string layer)
            : base($"({(!string.IsNullOrWhiteSpace(system) ? "system: " + system + ", " : "")}{(!string.IsNullOrWhiteSpace(layer) ? "layer: " + layer : "")}")
        {
            
        }
    }
}
