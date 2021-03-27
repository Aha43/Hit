using System;

namespace Hit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Test : Attribute
    {
        public string TestName { get; }

        public Test(string name = default)
        {
            TestName = name;
        }

    }

}
