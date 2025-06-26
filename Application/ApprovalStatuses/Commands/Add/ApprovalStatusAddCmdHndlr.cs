using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalStatuses.Commands.Add
{
    public class ApprovalStatusAddCmdHndlr : ICommandHandler<ApprovalStatusAddCmd,bool>
    {
        private readonly IApprovalStatusRepository _approvalStatusRepository;
        public ApprovalStatusAddCmdHndlr(IApprovalStatusRepository approvalStatusRepository) 
        {
            _approvalStatusRepository = approvalStatusRepository; 
        }

        public async Task<bool> Handle(ApprovalStatusAddCmd command) 
        {
            if (command.ApprovalStatusDto.Name == null) { return false; }

            var ApprovalStatus = new Domain.Entities.ApprovalStatus(command.ApprovalStatusDto.Name);
            await _approvalStatusRepository.Add(ApprovalStatus);
            return true;
        }
    }
}
