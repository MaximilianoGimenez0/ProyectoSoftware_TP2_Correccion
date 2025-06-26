using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.ProjectApprovalSteps.Commands.Add;
using Domain.Interfaces;

namespace Application.ProjectApprovalSteps.Commands.Update
{
    public class ProjectApprovalStepUpdateCmdHndlr : ICommandHandler<ProjectApprovalStepUpdateCmd,bool>
    {
        private readonly IProjectApprovalStepRepository _projectApprovalStepRepository;
        private readonly IProjectProposalRepository _projectProposalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApproverRoleRepository _approverRoleRepository;
        private readonly IApprovalStatusRepository _approvalStatusRepository;

        public ProjectApprovalStepUpdateCmdHndlr(IProjectApprovalStepRepository projectApprovalStepRepository, IProjectProposalRepository projectProposalRepository, IUserRepository userRepository, IApproverRoleRepository approverRoleRepository, IApprovalStatusRepository approvalStatusRepository)
        {
            _projectApprovalStepRepository = projectApprovalStepRepository;
            _projectProposalRepository = projectProposalRepository;
            _userRepository = userRepository;
            _approverRoleRepository = approverRoleRepository;
            _approvalStatusRepository = approvalStatusRepository;
        }

        public async Task<bool> Handle(ProjectApprovalStepUpdateCmd command)
        {
            var dto = command.ProjectApprovalStepDto;
            var requiredFields = new object?[]
            {
                dto.Id, 
                dto.ProjectProposalId,
                dto.ApproverRoleId,
                dto.Status,
                dto.StepOrder
            };


            if (requiredFields.Any(f => f == null)) return false;


            //Comprobación Project
            var projectId = dto.ProjectProposalId;
            var project = await _projectProposalRepository.GetById(projectId);
            if (project == null) { return false; }


            //Comprobación ApproverUser
            if (dto.ApproverUserId != null)
            {
                var approverUserId = dto.ProjectProposalId;
                var approverUser = await _projectProposalRepository.GetById(approverUserId);
                if (approverUser == null) { return false; }
            }


            //Comprobación ApproverRole
            var approverRoleId = dto.ApproverRoleId;
            var approverRole = await _approverRoleRepository.GetById(approverRoleId);
            if (approverRole == null) { return false; }


            //Comprobación status
            var statusId = dto.Status;
            var status = await _approvalStatusRepository.GetById(statusId);
            if (status == null) { return false; }

            return await _projectApprovalStepRepository.Update(dto);
           


        }
    }
}
