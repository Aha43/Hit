using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HitTest : Attribute
    {
    }
}
