using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalRules.Comands.Add
{
    public class ApprovalRuleAddCmd
    {
        public ApprovalRuleDto ApprovalRuleDto { get; set; }

        public ApprovalRuleAddCmd (ApprovalRuleDto approvalRuleDto)
        {
            ApprovalRuleDto = approvalRuleDto;
        }
    }
}
