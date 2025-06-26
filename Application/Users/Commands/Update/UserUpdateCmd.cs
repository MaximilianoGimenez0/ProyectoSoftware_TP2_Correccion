using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Users.Commands.Update
{
    public class UserUpdateCmd
    {
        public Domain.Entities.User UserDto { get; set; }

        public UserUpdateCmd(Domain.Entities.User userDto)
        {
            UserDto = userDto;
        }
    }
}
