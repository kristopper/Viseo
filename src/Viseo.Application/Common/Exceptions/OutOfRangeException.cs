using System;

namespace Viseo.Application.Common.Exceptions
{
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException(string fieldName, int usertype)
            : base($"{fieldName} \"{usertype}\" out of Ranged.")
        {
        }
    }
}