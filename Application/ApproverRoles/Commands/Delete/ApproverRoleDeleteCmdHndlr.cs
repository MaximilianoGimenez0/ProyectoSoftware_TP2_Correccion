using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApproverRole.Commands.Delete
{
    public class ApproverRoleDeleteCmdHndlr : ICommandHandler<ApproverRoleDeleteCmd,bool>
    {
        private readonly IApproverRoleRepository _roleRepository;

        public ApproverRoleDeleteCmdHndlr(IApproverRoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(ApproverRoleDeleteCmd command) 
        {
            if (command.ApproverRoleDto.Id == null) { return false; }

            var id = command.ApproverRoleDto.Id.Value;

            var exists = await _roleRepository.GetById(id);
            if (exists == null) return false;

            return await _roleRepository.Delete(id); 
            
        }
    }
}
