using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Users.Commands.Delete
{
    public class UserDeleteCmd
    {
        public UserDto UserDto { get; set; }

        public UserDeleteCmd(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
