using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Users.Queries.GetAll
{
    public class UserGetAllQryHndlr : IQueryHandler<UserGetAllQry,List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UserGetAllQryHndlr(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(UserGetAllQry query) 
        {
            var users = await _userRepository.GetAll();
            var usersDto = new List<UserDto>();
            foreach (var domUser in users) 
            {
                var temp = new UserDto() 
                {
                    Id = domUser.Id,
                    Name = domUser.Name,
                    Email = domUser.Email,
                    Role = domUser.Role,
                };
                usersDto.Add(temp);
            }

            return usersDto;
        }
    }
}
