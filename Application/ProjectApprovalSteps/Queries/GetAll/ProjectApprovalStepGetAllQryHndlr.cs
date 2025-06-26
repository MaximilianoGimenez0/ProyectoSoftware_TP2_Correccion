using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ProjectApprovalSteps.Queries.GetAll
{
    public class ProjectApprovalStepGetAllQryHndlr : IQueryHandler<ProjectApprovalStepGetAllQry,List<ProjectApprovalStep>>
    {
        private readonly IProjectApprovalStepRepository _projectApprovalStepRepository;

        public ProjectApprovalStepGetAllQryHndlr(IProjectApprovalStepRepository projectApprovalStepRepository)
        {
            _projectApprovalStepRepository = projectApprovalStepRepository;
        }

        public async Task<List<ProjectApprovalStep>> Handle(ProjectApprovalStepGetAllQry query)
        {
            return await _projectApprovalStepRepository.GetAll(); // lista de entidades EF
            
        }
    }
}
