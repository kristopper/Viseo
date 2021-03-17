using System;

namespace Viseo.Application.Common.Exceptions
{
    public class UserPasswordNotCorrectException : Exception
    {
        public UserPasswordNotCorrectException(string username)
            : base($"Password not correct for user '{username}'")
        {
        }
    }
}