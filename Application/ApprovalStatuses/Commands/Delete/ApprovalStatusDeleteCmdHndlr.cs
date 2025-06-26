using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalStatuses.Commands.Delete
{
    public class ApprovalStatusDeleteCmdHndlr : ICommandHandler<ApprovalStatusDeleteCmd,bool>
    {
        private readonly IApprovalStatusRepository _approvalStatusRepository;

        public ApprovalStatusDeleteCmdHndlr(IApprovalStatusRepository approvalStatusRepository)
        {
            _approvalStatusRepository = approvalStatusRepository;
        }

        public async Task<bool> Handle(ApprovalStatusDeleteCmd command) 
        {
            if (command.ApprovalStatusDto.Id == null) { return false; }
            var id = command.ApprovalStatusDto.Id.Value;

            var exists = _approvalStatusRepository.GetById(id);
            if (exists == null) { return false; }

            return await _approvalStatusRepository.Delete(id);
        }
    }
}
