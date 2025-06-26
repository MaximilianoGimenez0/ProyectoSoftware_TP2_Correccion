using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectProposals.Commands.Update
{
    public class ProjectProposalUpdateCmd
    {
        public ProjectProposalUpdateCmd(Domain.Entities.ProjectProposal projectProposalDto)
        {
            ProjectProposalDto = projectProposalDto;
        }

        public Domain.Entities.ProjectProposal ProjectProposalDto { get; set; }
    }
}
