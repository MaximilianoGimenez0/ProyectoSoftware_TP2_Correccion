using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Users.Commands.Update
{
    public class UserUpdateCmdHndlr : ICommandHandler<UserUpdateCmd,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApproverRoleRepository _roleRepository;

        public UserUpdateCmdHndlr(IUserRepository userRepository, IApproverRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(UserUpdateCmd command)
        {
            //Compruebo si tengo todos los datos para actualizar
            if (command.UserDto.Id == null || command.UserDto.Role == null || command.UserDto.Email == null || command.UserDto.Name == null) { return false; }


            //Compruebo si existe para actualizar

            var user = await _userRepository.GetById(command.UserDto.Id);
            if (user == null) { return false; }


            //Compruebo si existe el Rol referenciado

            var role = _roleRepository.GetById(command.UserDto.Role);
            if (role == null) return false;


            return await _userRepository.Update(command.UserDto);


        }
    }
}
