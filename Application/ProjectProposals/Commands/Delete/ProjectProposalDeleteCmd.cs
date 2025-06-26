using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectProposals.Commands.Delete
{
    public class ProjectProposalDeleteCmd
    {
        public ProjectProposalDeleteCmd(ProjectProposalDto projectProposalDto)
        {
            ProjectProposalDto = projectProposalDto;
        }

        public ProjectProposalDto ProjectProposalDto { get; set; }
    }
}
