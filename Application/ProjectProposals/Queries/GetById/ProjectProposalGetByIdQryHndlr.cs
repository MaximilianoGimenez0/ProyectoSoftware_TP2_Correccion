using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ProjectProposals.Queries.GetById
{
    public class ProjectProposalGetByIdQryHndlr : IQueryHandler<ProjectProposalGetByIdQry, Domain.Entities.ProjectProposal?>
    {
        private readonly IProjectProposalRepository _projectProposalrepository;

        public ProjectProposalGetByIdQryHndlr(IProjectProposalRepository projectProposalrepository)
        {
            _projectProposalrepository = projectProposalrepository;
        }

        public async Task<Domain.Entities.ProjectProposal?> Handle(ProjectProposalGetByIdQry query)
        {
            if (query.ProjectProposalDto.Id == null) { return null; }

            var id = query.ProjectProposalDto.Id.Value;
            var project = await _projectProposalrepository.GetById(id);

            return project;
            
        }
    }
}
