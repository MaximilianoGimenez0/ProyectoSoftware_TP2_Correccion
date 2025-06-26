using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ProjectProposals.Commands.Add;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ProjectProposals.Commands.Update
{
    public class ProjectProposalUpdateCmdHndlr : ICommandHandler<ProjectProposalUpdateCmd,bool>
    {
        private readonly IProjectProposalRepository _projectProposalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApprovalStatusRepository _approvalStatusRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IAreaRepository _areaRepository;

        public ProjectProposalUpdateCmdHndlr(IProjectProposalRepository projectProposalRepository, IUserRepository userRepository, IApprovalStatusRepository approvalStatusRepository, IProjectTypeRepository projectTypeRepository, IAreaRepository areaRepository)
        {
            _projectProposalRepository = projectProposalRepository;
            _userRepository = userRepository;
            _approvalStatusRepository = approvalStatusRepository;
            _projectTypeRepository = projectTypeRepository;
            _areaRepository = areaRepository;
        }

        public async Task<bool> Handle(ProjectProposalUpdateCmd command)
        {
            var dto = command.ProjectProposalDto;
            var requiredFields = new object?[]
            {
                dto.Id,
                dto.Title,
                dto.Description,
                dto.EstimatedAmount,
                dto.EstimatedDuration,
                dto.CreateAt,
                dto.Area,
                dto.Type,
                dto.Status,
                dto.CreateBy
            };


            if (requiredFields.Any(f => f == null)) return false;


            //Comprobación de fk con Area
            var areaId = command.ProjectProposalDto.Area;
            var area = await _areaRepository.GetById(areaId);
            if (area == null) { return false; }

            //Comprobación de fk con Type
            var typeId = command.ProjectProposalDto.Type;
            var type = await _projectTypeRepository.GetById(typeId);
            if (type == null) { return false; }

            //Comprobación con fk de Status
            var statusId = command.ProjectProposalDto.Status;
            var status = await _approvalStatusRepository.GetById(statusId);
            if (status == null) { return false; }

            //Comprobación con fk de User
            var userId = command.ProjectProposalDto.CreateBy;
            var user = await _userRepository.GetById(userId);
            if (user == null) { return false; }

            return await _projectProposalRepository.Update(dto);
        }

    }
}
