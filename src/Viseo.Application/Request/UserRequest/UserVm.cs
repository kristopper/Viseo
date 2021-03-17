using System.Collections.Generic;

namespace Viseo.Application.Request.UserRequest
{
    public class UserVm
    {
        public int Total { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public List<UserDto> UserList { get; set;}

    }
}