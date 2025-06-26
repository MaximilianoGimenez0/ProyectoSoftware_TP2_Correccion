using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalRules.Comands.Delete
{
    public class ApprovalRuleDeleteCmdHndlr : ICommandHandler<ApprovalRuleDeleteCmd,bool>
    {
        private readonly IApprovalRuleRepository _approvalRuleRepository;

        public ApprovalRuleDeleteCmdHndlr(IApprovalRuleRepository approvalRuleRepository)
        {
            _approvalRuleRepository = approvalRuleRepository;
        }

        public async Task<bool> Handle(ApprovalRuleDeleteCmd command) 
        {
            if (command.ApprovalRuleDto.Id == null) { return false; }

            var id = command.ApprovalRuleDto.Id.Value;
            var exists = await _approvalRuleRepository.GetById(id);
            if (exists == null) { return false; }

            return await _approvalRuleRepository.Delete(id);
        }
    }
}
