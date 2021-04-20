using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure.Exceptions
{
    public class MoreThanOneUnitTestEventHandlerClassException : Exception
    {
        public MoreThanOneUnitTestEventHandlerClassException() : base("More than one unit test event handler class not allowed")
        {
        }
    }
}
