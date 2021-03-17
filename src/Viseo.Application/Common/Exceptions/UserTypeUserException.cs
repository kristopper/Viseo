using System;

namespace Viseo.Application.Common.Exceptions
{
    public class UserTypeUserException: Exception
    {
        public UserTypeUserException(string name)
            : base($"Usertype \"{name}\" Cannot Filter Other Usertype")
        {
        }
    }
}