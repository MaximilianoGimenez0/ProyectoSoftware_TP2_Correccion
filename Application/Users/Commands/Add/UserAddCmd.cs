using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Users.Commands.Add
{
    public class UserAddCmd
    {
        public Domain.Entities.User UserDto { get; set; }

        public UserAddCmd(Domain.Entities.User userDto)
        {
            UserDto = userDto;
        }
    }
}
