using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectProposals.Queries.GetById
{
    public class ProjectProposalGetByIdQry
    {
        public ProjectProposalGetByIdQry(ProjectProposalDto projectProposalDto)
        {
            ProjectProposalDto = projectProposalDto;
        }

        public ProjectProposalDto ProjectProposalDto { get; set; }

    }
}
