using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Users.Commands.Add;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ApproverRole.Commands.Add
{
    public class ApproverRoleAddCmdHndlr : ICommandHandler<ApproverRoleAddCmd,bool>
    {
        private readonly IApproverRoleRepository _roleRepository;

        public ApproverRoleAddCmdHndlr (IApproverRoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(ApproverRoleAddCmd command)
        {
            if (command.ApproverRoleDto.Name == null ) { return false; }

            await _roleRepository.Add(command.ApproverRoleDto);
            
            return true;
        }
    }
}
