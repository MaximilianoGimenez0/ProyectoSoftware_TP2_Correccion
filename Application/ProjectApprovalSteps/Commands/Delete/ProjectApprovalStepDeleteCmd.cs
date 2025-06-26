using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectApprovalSteps.Commands.Delete
{
    public class ProjectApprovalStepDeleteCmd
    {
        public ProjectApprovalStepDeleteCmd(ProjectApprovalStepDto projectApprovalStepDto)
        {
            ProjectApprovalStepDto = projectApprovalStepDto;
        }

        public ProjectApprovalStepDto ProjectApprovalStepDto { get; set; }
    }
}
