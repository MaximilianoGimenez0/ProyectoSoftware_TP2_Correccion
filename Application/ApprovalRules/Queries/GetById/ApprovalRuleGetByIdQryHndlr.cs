using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalRules.Queries.GetById
{
    public class ApprovalRuleGetByIdQryHndlr : IQueryHandler<ApprovalRuleGetByIdQry,ApprovalRuleDto?>
    {
        private readonly IApprovalRuleRepository _approvalRuleRepository;

        public ApprovalRuleGetByIdQryHndlr(IApprovalRuleRepository approvalRuleRepository)
        {
            _approvalRuleRepository = approvalRuleRepository;
        }

        public async Task<ApprovalRuleDto?> Handle(ApprovalRuleGetByIdQry query)
        {
            if (query.ApprovalRuleDto.Id == null) { return null; }

            var id = query.ApprovalRuleDto.Id.Value;

            var approvalRule = await _approvalRuleRepository.GetById(id);

            if (approvalRule == null) { return null; }
            
            var result = new ApprovalRuleDto() 
            {
               Id = id,
               MinAmount = approvalRule.MinAmount,
               MaxAmount = approvalRule.MaxAmount,
               StepOrder = approvalRule.StepOrder,
               Area = approvalRule.Area,
               Type = approvalRule.Type,
               ApproverRoleId = approvalRule.ApproverRoleId,
            };

            return result;
        }
    }
}
