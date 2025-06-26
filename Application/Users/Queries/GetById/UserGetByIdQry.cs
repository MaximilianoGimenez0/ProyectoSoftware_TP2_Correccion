using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Users.Queries.GetById
{
    public class UserGetByIdQry
    {
        public UserDto User { get; set; }

        public UserGetByIdQry(UserDto user) 
        {
            User = user;
        }
    }
}
