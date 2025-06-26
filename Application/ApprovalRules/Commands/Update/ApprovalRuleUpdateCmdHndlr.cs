using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApprovalRules.Comands.Add;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalRules.Comands.Update
{
    public class ApprovalRuleUpdateCmdHndlr : ICommandHandler<ApprovalRuleUpdateCmd,bool>
    {
        private readonly IApprovalRuleRepository _approvalRuleRepository;
        private readonly IApproverRoleRepository _approverRoleRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IAreaRepository _areaRepository;

        public ApprovalRuleUpdateCmdHndlr(IApprovalRuleRepository approvalRuleRepository, IApproverRoleRepository approverRoleRepository, IProjectTypeRepository projectTypeRepository, IAreaRepository areaRepository)
        {
            _approvalRuleRepository = approvalRuleRepository;
            _approverRoleRepository = approverRoleRepository;
            _projectTypeRepository = projectTypeRepository;
            _areaRepository = areaRepository;
        }

        public async Task<bool> Handle(ApprovalRuleUpdateCmd command)
        {
            //Verifico si se cumplen los requisitos minimos
            if (command.ApprovalRuleDto.Id == null || command.ApprovalRuleDto.MinAmount == null || command.ApprovalRuleDto.MaxAmount == null || command.ApprovalRuleDto.ApproverRoleId == null || command.ApprovalRuleDto.StepOrder == null) { return false; }

            //Comprobar si existe
            var approvalRuleId = command.ApprovalRuleDto.Id.Value;
            var approvalRule = await _approvalRuleRepository.GetById(approvalRuleId);
            if (approvalRule == null) { return false; }

            //Comprobación de fk con ApproverRole
            var approverRoleId = command.ApprovalRuleDto.ApproverRoleId.Value;
            var approverRole =  await _approverRoleRepository.GetById(approverRoleId);
            if (approverRole == null) { return false; }

            //Comprobación de fk con Area
            if (command.ApprovalRuleDto.Area != null)
            {
                var areaId = command.ApprovalRuleDto.Area.Value;
                var area = await _areaRepository.GetById(areaId);
                if (area == null) { return false; }
            }

            //Comprobación de fk con Type
            if (command.ApprovalRuleDto.Type != null)
            {
                var typeId = command.ApprovalRuleDto.Type.Value;
                var type = await _projectTypeRepository.GetById(typeId);
                if (type == null) { return false; }
            }

            //Mapeo de DTO a clase de DOMAIN

            var domainApprovalRule = new Domain.Entities.ApprovalRule(command.ApprovalRuleDto.MinAmount.Value, command.ApprovalRuleDto.MaxAmount.Value, command.ApprovalRuleDto.StepOrder.Value, command.ApprovalRuleDto.ApproverRoleId.Value)
            {
                Id = command.ApprovalRuleDto.Id.Value,
                Area = command.ApprovalRuleDto.Area,
                Type = command.ApprovalRuleDto.Type,

            };

            return await _approvalRuleRepository.Update(domainApprovalRule);
        }
    }
}
