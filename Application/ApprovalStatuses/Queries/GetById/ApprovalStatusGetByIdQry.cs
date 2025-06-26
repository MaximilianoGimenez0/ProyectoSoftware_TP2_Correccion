using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalStatuses.Queries.GetById
{
    public class ApprovalStatusGetByIdQry
    {
        public ApprovalStatusDto ApprovalStatusDto { get; set; }

        public ApprovalStatusGetByIdQry (ApprovalStatusDto approvalStatusDto)
        {
            ApprovalStatusDto = approvalStatusDto;
        }
    }
}
