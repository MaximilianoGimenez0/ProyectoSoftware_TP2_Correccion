using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectApprovalSteps.Commands.Add
{
    public class ProjectApprovalStepAddCmd
    {
        public ProjectApprovalStepAddCmd(Domain.Entities.ProjectApprovalStep projectApprovalStepDto)
        {
            ProjectApprovalStepDto = projectApprovalStepDto;
        }

        public Domain.Entities.ProjectApprovalStep ProjectApprovalStepDto { get; set; }
    }
}
