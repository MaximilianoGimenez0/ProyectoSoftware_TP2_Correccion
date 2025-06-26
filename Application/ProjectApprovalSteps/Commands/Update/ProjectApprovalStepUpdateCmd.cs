using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.ProjectApprovalSteps.Commands.Add;
using Domain.Interfaces;

namespace Application.ProjectApprovalSteps.Commands.Update
{
    public class ProjectApprovalStepUpdateCmd
    {
        public ProjectApprovalStepUpdateCmd(Domain.Entities.ProjectApprovalStep projectApprovalStepDto)
        {
            ProjectApprovalStepDto = projectApprovalStepDto;
        }

        public Domain.Entities.ProjectApprovalStep ProjectApprovalStepDto { get; set; }
    }
}
