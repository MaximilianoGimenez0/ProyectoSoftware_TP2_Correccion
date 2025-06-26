using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Users.Commands.Add
{
    public class UserAddCmdHndlr : ICommandHandler<UserAddCmd,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApproverRoleRepository _roleRepository;

        public UserAddCmdHndlr(IUserRepository userRepository, IApproverRoleRepository approverRoleRepository) 
        {
            _userRepository = userRepository;
            _roleRepository = approverRoleRepository;
        }

        public async Task<bool> Handle(UserAddCmd command)
        {
            //Compruebo si tengo todos los datos para agregar
            if (command.UserDto.Role == null || command.UserDto.Email == null || command.UserDto.Name == null) { return false; }

            //Compruebo si existe el Rol referenciado
            var role = await _roleRepository.GetById(command.UserDto.Role);
            if (role == null) return false;

            var user = command.UserDto;

            await _userRepository.Add(user);
            return true;
        }
    }
}
