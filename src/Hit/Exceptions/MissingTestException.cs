using System;

namespace Hit.Exceptions
{
    public class MissingTestException : Exception
    {
        public MissingTestException(string name, string from) : base($"Missing test {name} referred from test {from}") { }
    }
}
