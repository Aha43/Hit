using System;

namespace Hit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Follows : Attribute
    {
        public string ParentTestName { get; }

        public Follows(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            ParentTestName = name.Trim();
        }

    }

}
