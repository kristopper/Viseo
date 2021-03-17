using System;

namespace Viseo.Application.Common.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string fieldName, string name)
            : base($"User {fieldName} \"{name}\" was not found.")
        {
        }
    }
}