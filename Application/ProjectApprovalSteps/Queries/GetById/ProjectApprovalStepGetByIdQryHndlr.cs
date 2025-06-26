using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.ProjectApprovalSteps.Queries.GetAll;
using Domain.Interfaces;

namespace Application.ProjectApprovalSteps.Queries.GetById
{
    public class ProjectApprovalStepGetByIdQryHndlr : IQueryHandler<ProjectApprovalStepGetByIdQry, Domain.Entities.ProjectApprovalStep?>
    {
        private readonly IProjectApprovalStepRepository _projectApprovalStepRepository;

        public ProjectApprovalStepGetByIdQryHndlr(IProjectApprovalStepRepository projectApprovalStepRepository)
        {
            _projectApprovalStepRepository = projectApprovalStepRepository;
        }

        public async Task<Domain.Entities.ProjectApprovalStep?> Handle(ProjectApprovalStepGetByIdQry query)
        {
            if (query.ProjectApprovalStepDto.Id == null) { return null; }

            var id = query.ProjectApprovalStepDto.Id.Value;
            return await _projectApprovalStepRepository.GetById(id);

        }
    }
}
