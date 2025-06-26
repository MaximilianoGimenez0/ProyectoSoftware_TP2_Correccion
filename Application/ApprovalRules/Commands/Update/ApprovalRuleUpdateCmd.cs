using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApprovalRules.Comands.Update
{
    public class ApprovalRuleUpdateCmd
    {
        public ApprovalRuleDto ApprovalRuleDto { get; set; }

        public ApprovalRuleUpdateCmd(ApprovalRuleDto approvalRuleDto) 
        {
            ApprovalRuleDto = approvalRuleDto;
        }
    }
}
