using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ApprovalRules.Queries.GetAll
{
    public class ApprovalRuleGetAllQryHndlr : IQueryHandler<ApprovalRuleGetAllQry, List<ApprovalRuleDto>>
    {
        private readonly IApprovalRuleRepository _approvalRuleRepository;

        public ApprovalRuleGetAllQryHndlr(IApprovalRuleRepository approvalRuleRepository)
        {
            _approvalRuleRepository = approvalRuleRepository;
        }

        public async Task<List<ApprovalRuleDto>> Handle(ApprovalRuleGetAllQry query)
        {
            var approvalRules = await _approvalRuleRepository.GetAll(); 

            var dtoApprovalRules = new List<Application.Dtos.ApprovalRuleDto>();


            foreach (var approvalRule in approvalRules)
            {
                var temp = new Application.Dtos.ApprovalRuleDto()
                {
                   Id = approvalRule.Id,
                   MinAmount = approvalRule.MinAmount,
                   MaxAmount = approvalRule.MaxAmount,
                   StepOrder = approvalRule.StepOrder,
                   Area = approvalRule.Area,
                   Type = approvalRule.Type,
                   ApproverRoleId = approvalRule.ApproverRoleId,
                };
                dtoApprovalRules.Add(temp);
            }

            return dtoApprovalRules;
        }
    }
}