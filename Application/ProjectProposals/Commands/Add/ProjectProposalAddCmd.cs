using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.ProjectProposals.Commands.Add
{
    public class ProjectProposalAddCmd
    {
        public ProjectProposalAddCmd(ProjectProposal projectProposalDto)
        {
            ProjectProposalDto = projectProposalDto;
        }

        public ProjectProposal ProjectProposalDto { get; set; }
    }
}
