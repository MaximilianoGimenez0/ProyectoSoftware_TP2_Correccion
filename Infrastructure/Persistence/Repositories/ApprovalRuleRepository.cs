﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ApprovalRuleRepository : IApprovalRuleRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public ApprovalRuleRepository(ProjectApprovalDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.ApprovalRule approvalRule)
        {
            var efApprovalRule = new Infrastructure.Persistence.Entities.ApprovalRule()
            {
                MinAmount = approvalRule.MinAmount,
                MaxAmount = approvalRule.MaxAmount,
                StepOrder = approvalRule.StepOrder,
                Area = approvalRule.Area,
                Type = approvalRule.Type,
                ApproverRoleId = approvalRule.ApproverRoleId,

            };

           await _context.Rules.AddAsync(efApprovalRule);
           await _context.SaveChangesAsync();
           
        }

        public async Task<bool> Delete(long id)
        {
            var efApprovalRule = await _context.Rules.FirstOrDefaultAsync(r => r.Id == id);
            if (efApprovalRule == null) return false;

            _context.Rules.Remove(efApprovalRule);
            await _context.SaveChangesAsync();
            return true;
            
        }
            
        public async Task<List<Domain.Entities.ApprovalRule>> GetAll()
        {
            var efApprovalRules = await _context.Rules.ToListAsync();
            var domainApprovalRules = new List<Domain.Entities.ApprovalRule>();

            foreach (var efApprovalRule in efApprovalRules)
            {
                var approvalRule = new Domain.Entities.ApprovalRule(efApprovalRule.MinAmount,efApprovalRule.MaxAmount,efApprovalRule.StepOrder,efApprovalRule.ApproverRoleId) 
                {
                    Id = efApprovalRule.Id,
                    Area = efApprovalRule.Area,
                    Type = efApprovalRule.Type,
                };

                domainApprovalRules.Add(approvalRule);
            }
            return domainApprovalRules;
        }

        public async Task<Domain.Entities.ApprovalRule?> GetById(long id)
        {
            var approvalRule = await _context.Rules.FirstOrDefaultAsync(r => r.Id == id);
            return approvalRule == null ? null : new Domain.Entities.ApprovalRule(approvalRule.MinAmount, approvalRule.MaxAmount, approvalRule.StepOrder, approvalRule.ApproverRoleId)
            {
                Id = approvalRule.Id,
                Area = approvalRule.Area,
                Type = approvalRule.Type,
            };
        }

        public async Task<bool> Update(Domain.Entities.ApprovalRule approvalRule)
        {
            var id = approvalRule.Id;
            var approvalRuleDb = await _context.Rules.FirstOrDefaultAsync(r => r.Id == id);
            if (approvalRuleDb == null) return false;

            approvalRuleDb.MinAmount = approvalRule.MinAmount;
            approvalRuleDb.MaxAmount = approvalRule.MaxAmount;
            approvalRuleDb.StepOrder = approvalRule.StepOrder;
            approvalRuleDb.Area = approvalRule.Area;
            approvalRuleDb.Type = approvalRule.Type;
            approvalRuleDb.ApproverRoleId = approvalRule.ApproverRoleId;
            
            await _context.SaveChangesAsync();
            
            return true;

        }
    }
}
