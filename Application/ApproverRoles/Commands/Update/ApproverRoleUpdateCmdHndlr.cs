using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApproverRole.Commands.Update
{
    public class ApproverRoleUpdateCmdHndlr : ICommandHandler<ApproverRoleUpdateCmd,bool>
    {
        private readonly IApproverRoleRepository _roleRepository;

        public ApproverRoleUpdateCmdHndlr(IApproverRoleRepository roleRepository) { _roleRepository = roleRepository; }

        public async Task<bool> Handle(ApproverRoleUpdateCmd command)
        {
            if (command.ApproverRoleDto.Id == null || command.ApproverRoleDto.Name == null) { return false;}

            var id = command.ApproverRoleDto.Id;
            var result = await _roleRepository.GetById(id);

            if (result == null) return false;
            
            var approverRole = command.ApproverRoleDto;

            return await _roleRepository.Update(approverRole);
            

        }
    }
}
