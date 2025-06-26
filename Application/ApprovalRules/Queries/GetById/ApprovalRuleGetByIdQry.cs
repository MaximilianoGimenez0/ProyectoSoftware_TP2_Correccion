using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalRules.Queries.GetById
{
    public class ApprovalRuleGetByIdQry
    {
        public ApprovalRuleGetByIdQry(ApprovalRuleDto approvalRuleDto)
        {
            ApprovalRuleDto = approvalRuleDto;
        }

        public ApprovalRuleDto ApprovalRuleDto { get; set; }


    }
}
