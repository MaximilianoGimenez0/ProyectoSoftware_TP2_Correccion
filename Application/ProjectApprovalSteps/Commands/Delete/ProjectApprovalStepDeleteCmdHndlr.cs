using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ProjectApprovalSteps.Commands.Delete
{
    public class ProjectApprovalStepDeleteCmdHndlr : ICommandHandler<ProjectApprovalStepDeleteCmd,bool>
    {
        private readonly IProjectApprovalStepRepository _projectApprovalStepRepository;

        public ProjectApprovalStepDeleteCmdHndlr(IProjectApprovalStepRepository projectApprovalStepRepository)
        {
            _projectApprovalStepRepository = projectApprovalStepRepository;
        }

        public async Task<bool> Handle(ProjectApprovalStepDeleteCmd command)
        {
            if (command.ProjectApprovalStepDto.Id == null) { return false; }

            var steps = await _projectApprovalStepRepository.GetAll();

            if (steps.Any(s => s.Id == command.ProjectApprovalStepDto.Id.Value))
            {
                return false;
            }

            return await _projectApprovalStepRepository.Delete(command.ProjectApprovalStepDto.Id.Value);
        }
    }
}
