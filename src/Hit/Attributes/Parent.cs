using System;

namespace Hit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Parent : Attribute
    {
        public string ParentTestName { get; }

        public Parent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            ParentTestName = name.Trim();
        }

    }

}
