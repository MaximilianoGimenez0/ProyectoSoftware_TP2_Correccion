using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectApprovalSteps.Queries.GetById
{
    public class ProjectApprovalStepGetByIdQry
    {
        public ProjectApprovalStepGetByIdQry(ProjectApprovalStepDto projectApprovalStepDto)
        {
            ProjectApprovalStepDto = projectApprovalStepDto;
        }

        public ProjectApprovalStepDto ProjectApprovalStepDto { get; set; }
    }
}
