using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalStatuses.Commands.Update
{
    public class ApprovalStatusUpdateCmdHndlr:ICommandHandler<ApprovalStatusUpdateCmd,bool>
    {
        private readonly IApprovalStatusRepository _approvalStatusRepository;

        public ApprovalStatusUpdateCmdHndlr(IApprovalStatusRepository approvalStatusRepository)
        {
            _approvalStatusRepository = approvalStatusRepository;
        }

        public async Task<bool> Handle(ApprovalStatusUpdateCmd command) 
        {
            if (command.ApprovalStatusDto.Id == null || command.ApprovalStatusDto.Name == null) { return false; }

            var id = command.ApprovalStatusDto.Id.Value;
            var exists = await _approvalStatusRepository.GetById(id);
            if (exists == null) { return false; }

            var ApprovalStatus = new Domain.Entities.ApprovalStatus(command.ApprovalStatusDto.Name) { Id = id };
            await _approvalStatusRepository.Update(ApprovalStatus);
            return true;
        }
    }
}
