using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.Commands.Add;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalRules.Comands.Add
{
    public class ApprovalRuleAddCmdHndlr : ICommandHandler<ApprovalRuleAddCmd,bool>
    {
        private readonly IApprovalRuleRepository _approvalRuleRepository;
        private readonly IApproverRoleRepository _approverRoleRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IAreaRepository _areaRepository;

        public ApprovalRuleAddCmdHndlr (IApprovalRuleRepository approvalRuleRepository, IApproverRoleRepository approverRoleRepository, IProjectTypeRepository projectTypeRepository, IAreaRepository areaRepository)
        {
            _approvalRuleRepository = approvalRuleRepository;
            _approverRoleRepository = approverRoleRepository;
            _projectTypeRepository = projectTypeRepository;
            _areaRepository = areaRepository;
        }

        public async Task<bool> Handle(ApprovalRuleAddCmd command)
        {
            //Verifico si se cumplen los requisitos minimos
            if (command.ApprovalRuleDto.MinAmount == null || command.ApprovalRuleDto.MaxAmount == null || command.ApprovalRuleDto.ApproverRoleId == null || command.ApprovalRuleDto.StepOrder == null) { return false; }

            //Comprobación de fk con ApproverRole
            var approverRoleId = command.ApprovalRuleDto.ApproverRoleId.Value;
            var approverRole = await _approverRoleRepository.GetById(approverRoleId);
            if (approverRole == null) { return false; }

            //Comprobación de fk con Area
            if (command.ApprovalRuleDto.Area != null) 
            { 
            var areaId = command.ApprovalRuleDto.Area.Value;
            var area = await _areaRepository.GetById (areaId);
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
                Area = command.ApprovalRuleDto.Area,
                Type = command.ApprovalRuleDto.Type,

            };

            await _approvalRuleRepository.Add(domainApprovalRule);
            return true;
        }
    }
}
