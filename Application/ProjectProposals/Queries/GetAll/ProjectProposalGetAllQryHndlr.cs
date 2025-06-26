using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.ProjectTypes.Queries.GetAll;
using Domain.Interfaces;

namespace Application.ProjectProposals.Queries.GetAll
{
    public class ProjectProposalGetAllQryHndlr : IQueryHandler<ProjectProposalGetAllQry,List<ProjectProposalDto>>
    {
        private readonly IProjectProposalRepository _projectProposalrepository;

        public ProjectProposalGetAllQryHndlr(IProjectProposalRepository projectProposalrepository)
        {
            _projectProposalrepository = projectProposalrepository;
        }

        public async Task<List<ProjectProposalDto>> Handle(ProjectProposalGetAllQry query)
        {
            var projectProposals = await _projectProposalrepository.GetAll();

            var projectProposalsDtos = new List<Application.Dtos.ProjectProposalDto>();


            foreach (var projectProposal in projectProposals)
            {
                var temp = new Application.Dtos.ProjectProposalDto()
                {
                    Id = projectProposal.Id,
                    Title = projectProposal.Title,
                    Description = projectProposal.Description,
                    EstimatedAmount = projectProposal.EstimatedAmount,
                    EstimatedDuration = projectProposal.EstimatedDuration,
                    CreateAt = projectProposal.CreateAt,
                    Area = projectProposal.Area,
                    Type = projectProposal.Type,
                    Status = projectProposal.Status,
                    CreateBy = projectProposal.CreateBy,
                };
                projectProposalsDtos.Add(temp);
            }

            return projectProposalsDtos;
        }
    }
}
