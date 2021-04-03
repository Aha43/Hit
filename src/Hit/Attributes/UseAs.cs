using System;

namespace Hit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class UseAs : Attribute
    {
        public string Name { get; }

        public string Follows { get; }

        public UseAs(string test) => Name = test;

        public UseAs(string test, string followingTest)
        {
            Name = test;
            Follows = followingTest;
        }

    }

}
