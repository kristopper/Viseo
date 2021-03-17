using System;

namespace Viseo.Application.Common.Exceptions
{
    public class CannotUpdateException: Exception
    {
        public CannotUpdateException(string name)
            : base($"Usertype \"{name}\" Cannot Update Other User")
        {
        }
    }
}