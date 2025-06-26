using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalRules.Comands.Delete
{
    public class ApprovalRuleDeleteCmd
    {
        public ApprovalRuleDto ApprovalRuleDto { get; set; }
        public ApprovalRuleDeleteCmd(ApprovalRuleDto approvalRuleDto) 
        {
            ApprovalRuleDto = approvalRuleDto;
        }
    }
}
