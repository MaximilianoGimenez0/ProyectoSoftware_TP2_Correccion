using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Application.ApproverRole.Commands.Delete;
using Application.Dtos;
using Application.Interfaces;
using Application.Users.Commands.Add;
using Domain.Interfaces;

namespace Application.Users.Commands.Delete
{
    public class UserDeleteCmdHndlr : ICommandHandler<UserDeleteCmd,bool>
    {
        private readonly IUserRepository _userRepository;

        public UserDeleteCmdHndlr(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UserDeleteCmd command)
        {
            if (command.UserDto.Id == null) { return false; }

            //Compruebo si existe el usuario a eliminar
            var id = command.UserDto.Id;
            var result = await _userRepository.GetById(id.Value);

            if (result == null) return false;

            return await _userRepository.Delete(id.Value);
            
        }


    }
}
