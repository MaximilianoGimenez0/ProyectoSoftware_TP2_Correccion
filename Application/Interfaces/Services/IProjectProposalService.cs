using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;

namespace Application.Interfaces.Services
{
    public interface IProjectProposalService
    {
        Task<ProjectResponse> present(ProjectProposalDto project);
        
        Task<List<ProjectProposalViewDto>> ViewAll();
        Task<ProjectResponse> GetCompleteProjectGetById(Guid id);
        Task<ProjectResponse> UpdateProject(Guid id, ProjectUpdateRequest update);
        Task<List<ProjectShortResponse>> GetFiltered(string? title,int? status,int? createBy,int? approverRole);
        Task<ProjectResponse> UpdateProjectStep(Guid id, updateApprovalStatusDto dto);

    }
}
