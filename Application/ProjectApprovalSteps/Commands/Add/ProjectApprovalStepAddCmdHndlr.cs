using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ProjectApprovalSteps.Commands.Add
{
    public class ProjectApprovalStepAddCmdHndlr : ICommandHandler<ProjectApprovalStepAddCmd,long?>
    {
        private readonly IProjectApprovalStepRepository _projectApprovalStepRepository;
        private readonly IProjectProposalRepository _projectProposalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApproverRoleRepository _approverRoleRepository;
        private readonly IApprovalStatusRepository _approvalStatusRepository;

        public ProjectApprovalStepAddCmdHndlr(IProjectApprovalStepRepository projectApprovalStepRepository, IProjectProposalRepository projectProposalRepository, IUserRepository userRepository, IApproverRoleRepository approverRoleRepository, IApprovalStatusRepository approvalStatusRepository)
        {
            _projectApprovalStepRepository = projectApprovalStepRepository;
            _projectProposalRepository = projectProposalRepository;
            _userRepository = userRepository;
            _approverRoleRepository = approverRoleRepository;
            _approvalStatusRepository = approvalStatusRepository;
        }

        public async Task<long?> Handle(ProjectApprovalStepAddCmd command)
        {
            var dto = command.ProjectApprovalStepDto;
            var requiredFields = new object?[]
            {
                dto.ProjectProposalId,
                dto.ApproverRoleId,
                dto.Status,
                dto.StepOrder
            };


            if (requiredFields.Any(f => f == null)) return null;


            //Comprobación Project
            var projectId = dto.ProjectProposalId;
            var project = await _projectProposalRepository.GetById(projectId);
            if (project == null) { return null; }


            //Comprobación ApproverUser
            if (dto.ApproverUserId != null)
            {
                var approverUserId = dto.ProjectProposalId;
                var approverUser = await _projectProposalRepository.GetById(approverUserId);
                if (approverUser == null) { return null; }
            }


            //Comprobación ApproverRole
            var approverRoleId = dto.ApproverRoleId;
            var approverRole = await _approverRoleRepository.GetById(approverRoleId);
            if (approverRole == null) { return null; }


            //Comprobación status
            var statusId = dto.Status;
            var status = await _approvalStatusRepository.GetById(statusId);
            if (status == null) { return null; }


            
            return await _projectApprovalStepRepository.Add(dto);


        }
    }
}
