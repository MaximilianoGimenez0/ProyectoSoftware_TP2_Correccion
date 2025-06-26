using System;
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
    public class ProjectApprovalStepRepository : IProjectApprovalStepRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public ProjectApprovalStepRepository(ProjectApprovalDbContext context) 
        {
            _context = context;
        }

        public async Task<long> Add(Domain.Entities.ProjectApprovalStep step)
        {
           
            var efStep = new Infrastructure.Persistence.Entities.ProjectApprovalStep()
            {
                StepOrder = step.StepOrder,
                DecisionDate = step.DecisionDate,
                Observations = step.Observations,
                ProjectProposalId = step.ProjectProposalId,
                ApproverUserId = step.ApproverUserId,
                ApproverRoleId = step.ApproverRoleId,
                Status = step.Status,
            };

            
            await _context.ProjectApprovalSteps.AddAsync(efStep);
            await _context.SaveChangesAsync();

            return efStep.Id;
            
        }

        public async Task<bool> Delete(long id)
        {
            var efStep = await _context.ProjectApprovalSteps.FirstOrDefaultAsync(s => s.Id == id);
            if (efStep == null) return false;

            _context.ProjectApprovalSteps.Remove(efStep);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<List<Domain.Entities.ProjectApprovalStep>> GetAll()
        {
            var efSteps = await _context.ProjectApprovalSteps.ToListAsync(); // lista de entidades EF

            var domainSteps = new List<Domain.Entities.ProjectApprovalStep>();


            foreach (var efStep in efSteps)
            {
                var step = new Domain.Entities.ProjectApprovalStep()
                {
                    Status = efStep.Status,
                    ApproverRoleId = efStep.ApproverRoleId,
                    ProjectProposalId = efStep.ProjectProposalId,
                    StepOrder = efStep.StepOrder,
                    Id = efStep.Id,
                    DecisionDate = efStep.DecisionDate,
                    Observations = efStep.Observations,
                    ApproverUserId = efStep.ApproverUserId
                };
                domainSteps.Add(step);
            }

            return domainSteps;
        }

        public async Task<Domain.Entities.ProjectApprovalStep?> GetById(long id)
        {
            var efStep = await _context.ProjectApprovalSteps.FirstOrDefaultAsync(s => s.Id == id);
            return efStep == null ? null : new Domain.Entities.ProjectApprovalStep()
            {
                StepOrder = efStep.StepOrder,
                ProjectProposalId =efStep.ProjectProposalId,
                ApproverRoleId=efStep.ApproverRoleId,
                Status = efStep.Status,
                Id = efStep.Id,
                DecisionDate = efStep.DecisionDate,
                Observations = efStep.Observations,
                ApproverUserId = efStep.ApproverUserId
            };
        }

        public async Task<bool> Update(Domain.Entities.ProjectApprovalStep step)
        {
            var id = step.Id;

            var efStep = await _context.ProjectApprovalSteps.FirstOrDefaultAsync(s => s.Id == id);
            if (efStep == null) return false;
           
            efStep.StepOrder = step.StepOrder;
            efStep.DecisionDate = step.DecisionDate;
            efStep.Observations = step.Observations;
            efStep.ProjectProposalId = step.ProjectProposalId;
            efStep.ApproverUserId = step.ApproverUserId;
            efStep.ApproverRoleId = step.ApproverRoleId;
            efStep.Status = step.Status;
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
