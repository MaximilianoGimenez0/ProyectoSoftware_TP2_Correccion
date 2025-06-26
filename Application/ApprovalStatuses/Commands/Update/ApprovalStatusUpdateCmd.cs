using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalStatuses.Commands.Update
{
    public class ApprovalStatusUpdateCmd
    {
        public ApprovalStatusDto ApprovalStatusDto { get; set; }

        public ApprovalStatusUpdateCmd(ApprovalStatusDto approvalStatusDto) 
        {
            ApprovalStatusDto = approvalStatusDto;
        }
    }
}
