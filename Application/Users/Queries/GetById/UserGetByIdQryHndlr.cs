using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.Users.Queries.GetAll;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Users.Queries.GetById
{
    public class UserGetByIdQryHndlr : IQueryHandler<UserGetByIdQry,User?>
    {
        private readonly IUserRepository _userRepository;

        public UserGetByIdQryHndlr(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(UserGetByIdQry query)
        {
            if (query.User.Id == null) { return null; }

            var user = await _userRepository.GetById(query.User.Id.Value);

            if (user == null) { return null; }

            return user;
        }
    }
}
