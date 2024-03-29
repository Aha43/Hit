﻿using Hit.Infrastructure.Exceptions;
using System;

namespace Hit.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class UseAs : Attribute
    {
        public string Name { get; }

        public string Follows { get; }

        public string Options { get; set; }

        private string _unitTest = string.Empty;
        public string UnitTest
        {
            get => (_unitTest.Length == 0) ? null : (_unitTest.Equals("!") ? Name : _unitTest);
            set => _unitTest = (value == null) ? string.Empty : value.Trim();
        }

        public UseAs(string test)
        {
            if (string.IsNullOrWhiteSpace(test))
            {
                throw new TestNameNullOrSpacesException();
            }

            Name = test.Trim();
        }

        public UseAs(string test, string followingTest)
        {
            if (string.IsNullOrWhiteSpace(test))
            {
                throw new TestNameNullOrSpacesException();
            }
            if (string.IsNullOrWhiteSpace(followingTest))
            {
                throw new FollowingTestNameNullOrSpacesException();
            }

            Name = test.Trim();
            Follows = followingTest.Trim();
        }

    }

}
