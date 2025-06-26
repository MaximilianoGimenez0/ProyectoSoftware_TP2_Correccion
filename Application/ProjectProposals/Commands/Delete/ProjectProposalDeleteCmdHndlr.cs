using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ProjectProposals.Commands.Delete
{
    public class ProjectProposalDeleteCmdHndlr : ICommandHandler<ProjectProposalDeleteCmd,bool>
    {
        private readonly IProjectProposalRepository _projectProposalRepository;
        private readonly IProjectApprovalStepRepository _projectApprovalStepRepository;

        public ProjectProposalDeleteCmdHndlr(IProjectProposalRepository projectProposalRepository, IProjectApprovalStepRepository projectApprovalStepRepository)
        {
            _projectProposalRepository = projectProposalRepository;
            _projectApprovalStepRepository = projectApprovalStepRepository;
        }

        public async Task<bool> Handle(ProjectProposalDeleteCmd command) 
        {
            if (command.ProjectProposalDto.Id == null) { return false; }

            //Compruebo si existe antes de borrar
            var existsProposal = await _projectProposalRepository.GetById(command.ProjectProposalDto.Id.Value);
            if (existsProposal == null) { return false; }

            //Compruebo hay algún approvalStep que lo tenga asociado
            var steps = await _projectApprovalStepRepository.GetAll();
            if (steps.Any(s => s.ProjectProposalId == command.ProjectProposalDto.Id))
            {
               return false;
            }

            return await _projectProposalRepository.Delete(command.ProjectProposalDto.Id.Value);
        }
    }
}
