using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectProposalRepository : IProjectProposalRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public ProjectProposalRepository(ProjectApprovalDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> Add(Domain.Entities.ProjectProposal project)
        {

            var exists = await _context.ProjectProposals.AnyAsync(p => p.Title == project.Title);
            if (exists)
                throw new BadRequestException("Ya existe una propuesta con ese título");


            var steps = new List<Entities.ProjectApprovalStep>();

            foreach (var step in project.Steps) 
            {
                var temp = new Entities.ProjectApprovalStep 
                { 
                    Status = step.Status,
                    StepOrder = step.StepOrder,
                    ProjectProposalId = step.ProjectProposalId,
                    ApproverRoleId = step.ApproverRoleId
                };
                
                steps.Add(temp);
            }

            // Mapeo a EF
            var efProject = new Infrastructure.Persistence.Entities.ProjectProposal()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                EstimatedAmount = project.EstimatedAmount,
                EstimatedDuration = project.EstimatedDuration,
                CreateAt = project.CreateAt,
                Area = project.Area,
                Type = project.Type,
                Status = project.Status,
                CreateBy = project.CreateBy,
                ProjectApprovalSteps = steps

            };

            await _context.ProjectProposals.AddAsync(efProject);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> Delete(Guid id)
        {
            var efProject = await _context.ProjectProposals.FirstOrDefaultAsync(p => p.Id == id);
            if (efProject == null) return false;

            _context.ProjectProposals.Remove(efProject);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Domain.Entities.ProjectProposal>> GetAll()
        {
            var efProjects = await _context.ProjectProposals.ToListAsync(); // lista de entidades EF


            var domainProjects = new List<Domain.Entities.ProjectProposal>();


            foreach (var efProject in efProjects)
            {
                var project = new Domain.Entities.ProjectProposal()
                {
                    Title = efProject.Title,
                    Description = efProject.Description,
                    EstimatedAmount = efProject.EstimatedAmount,
                    EstimatedDuration = efProject.EstimatedDuration,
                    CreateAt = efProject.CreateAt,
                    Area = efProject.Area,
                    Type = efProject.Type,
                    Status = efProject.Status,
                    CreateBy = efProject.CreateBy,
                    Id = efProject.Id,
                };
                domainProjects.Add(project);
            }

            return domainProjects;
        }

        public async Task<Domain.Entities.ProjectProposal?> GetById(Guid id)
        {
            var efProject = await _context.ProjectProposals.FirstOrDefaultAsync(p => p.Id == id);

            if (efProject == null) { return null; }

            var result = new Domain.Entities.ProjectProposal()
            { 
                Id = efProject.Id,
                Title = efProject.Title,
                Description = efProject.Description,
                EstimatedAmount = efProject.EstimatedAmount,
                EstimatedDuration= efProject.EstimatedDuration,
                CreateAt = efProject.CreateAt,
                Area = efProject.Area,
                Type = efProject.Type,
                Status = efProject.Status,
                CreateBy= efProject.CreateBy
                
            };
            
            return result;
        }

        public async Task<bool> Update(Domain.Entities.ProjectProposal project)
        {
            var id = project.Id;

            var efProject = _context.ProjectProposals.FirstOrDefault(p => p.Id == id);
            if (efProject == null) return false;

           efProject.Title = project.Title;
           efProject.Description = project.Description;
           efProject.EstimatedAmount = project.EstimatedAmount;
           efProject.EstimatedDuration = project.EstimatedDuration;
           efProject.CreateAt = project.CreateAt;
           efProject.Area = project.Area;
           efProject.Type = project.Type;
           efProject.Status = project.Status;
           efProject.CreateBy = project.CreateBy;
           
           await _context.SaveChangesAsync();
           return true;

        }
    }
}
