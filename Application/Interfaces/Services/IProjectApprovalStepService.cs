using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using static Application.Services.ProjectApprovalStepService;

namespace Application.Interfaces.Services
{
    public interface IProjectApprovalStepService
    {
        Task<List<Domain.Entities.ProjectApprovalStep>> GetProjectSteps(Guid id);
        Task<ProjectApprovalStepDto?> ViewNextProjectApprvalStep(Guid id);
        Task<List<ProjectApprovalStepDto>> ViewPendingAll();
        Task<List<Domain.Entities.ProjectApprovalStep>> CalculateSteps(Domain.Entities.ProjectProposal project);
        Task<bool> UpdateStatus(updateApprovalStatusDto dto);
    }
}
