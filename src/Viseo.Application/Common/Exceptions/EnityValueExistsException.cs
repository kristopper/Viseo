using System;

namespace Viseo.Application.Common.Exceptions
{
    public class EnityValueExistsException : Exception
    {
        public EnityValueExistsException(string fieldName, string name)
            : base($"User {fieldName} \"{name}\" is Already Exists.")
        {
        }
    }
}