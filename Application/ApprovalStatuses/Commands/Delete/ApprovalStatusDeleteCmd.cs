using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalStatuses.Commands.Delete
{
    public class ApprovalStatusDeleteCmd
    {
        public ApprovalStatusDto ApprovalStatusDto { get; set; }

        public ApprovalStatusDeleteCmd(ApprovalStatusDto approvalStatusDto) 
        {
            ApprovalStatusDto = approvalStatusDto; 
        }
    }
}
