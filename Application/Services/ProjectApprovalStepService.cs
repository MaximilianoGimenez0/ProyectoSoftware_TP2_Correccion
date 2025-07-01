using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApprovalRules.Queries.GetAll;
using Application.ApprovalStatuses.Queries.GetById;
using Application.ApproverRole.Queries.GetALL;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ProjectApprovalSteps.Commands.Add;
using Application.ProjectApprovalSteps.Commands.Update;
using Application.ProjectApprovalSteps.Queries.GetAll;
using Application.ProjectApprovalSteps.Queries.GetById;
using Application.ProjectProposals.Commands.Update;
using Application.ProjectProposals.Queries.GetById;
using Application.Users.Queries.GetById;
using Domain.Entities;

namespace Application.Services
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IQueryHandler<ProjectApprovalStepGetAllQry,List<ProjectApprovalStep>> _projectApprovalStepGetAllHandler;
        private readonly IQueryHandler<ApprovalRuleGetAllQry,List<ApprovalRuleDto>> _approvalRuleGetAllHandler;

        private readonly IQueryHandler<ProjectApprovalStepGetByIdQry, ProjectApprovalStep?> _projectApprovalStepGetByIdHandler;
        private readonly IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatus?> _approvalStatusGetByIdHandler;
        private readonly IQueryHandler<ProjectProposalGetByIdQry, ProjectProposal?> _projectProposalGetByIdHandler;
        private readonly IQueryHandler<UserGetByIdQry, User?> _userGetByIdHandler;
        
        private readonly ICommandHandler<ProjectApprovalStepUpdateCmd,bool> _projectApprovalStepUpdateHander;
        private readonly ICommandHandler<ProjectProposalUpdateCmd,bool> _projectProposalUpdateHander;

        public ProjectApprovalStepService
        (
            IQueryHandler<ApprovalRuleGetAllQry, List<ApprovalRuleDto>> approvalRuleGetAllHandler,
            IQueryHandler<ProjectApprovalStepGetAllQry, List<ProjectApprovalStep>> projectApprovalStepGetAllHandler,
            IQueryHandler<UserGetByIdQry, User?> userGetByIdHandler,
            IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatus?> approvalStatusGetByIdHandler,
            IQueryHandler<ProjectApprovalStepGetByIdQry, ProjectApprovalStep?> projectApprovalStepGetByIdHandler,
            ICommandHandler<ProjectApprovalStepUpdateCmd, bool> projectApprovalStepUpdateHander,
            IQueryHandler<ProjectProposalGetByIdQry, ProjectProposal?> projectProposalGetByIdHandler,
            ICommandHandler<ProjectProposalUpdateCmd, bool> projectProposalUpdateHander
        )
        {
            _approvalRuleGetAllHandler = approvalRuleGetAllHandler;
           
            _projectApprovalStepGetAllHandler = projectApprovalStepGetAllHandler;
            _userGetByIdHandler = userGetByIdHandler;
            _approvalStatusGetByIdHandler = approvalStatusGetByIdHandler;
            _projectApprovalStepGetByIdHandler = projectApprovalStepGetByIdHandler;
            _projectApprovalStepUpdateHander = projectApprovalStepUpdateHander;
            _projectProposalGetByIdHandler = projectProposalGetByIdHandler;
            _projectProposalUpdateHander = projectProposalUpdateHander;
        }
               

        public async Task<List<ProjectApprovalStepDto>> ViewPendingAll()
        {
            var pendingSteps = new List<ProjectApprovalStepDto>();

            var steps = await _projectApprovalStepGetAllHandler.Handle(new ProjectApprovalStepGetAllQry());

            foreach (var step in steps)
            {
                if (step.Status == 1)
                {
                    var temp = new ProjectApprovalStepDto()
                    {
                        Id = step.Id,
                        ProjectProposalId = step.ProjectProposalId,
                        StepOrder = step.StepOrder,
                        Status = step.Status,
                        ApproverRoleId = step.ApproverRoleId,
                        DecisionDate = step.DecisionDate,
                        Observations = step.Observations
                    };

                    pendingSteps.Add(temp);
                }
            }

            return pendingSteps;
        }

        
        public async Task<List<ProjectApprovalStep>> CalculateSteps(ProjectProposal project)
        {
            var rules = await _approvalRuleGetAllHandler.Handle(new ApprovalRuleGetAllQry());

            // 1. Filtro por las reglas 
            var applicableRules = rules
                .Where(rule =>
                    rule.MinAmount <= project.EstimatedAmount &&
                    (rule.MaxAmount == 0 || rule.MaxAmount >= project.EstimatedAmount) &&
                    (rule.Area == null || rule.Area == project.Area) &&
                    (rule.Type == null || rule.Type == project.Type)
                )
                .ToList();

            var groupedByStep = applicableRules
            .GroupBy(rule => rule.StepOrder)
            .OrderBy(g => g.Key) 
            .ToList();

            var selectedSteps = new List<ProjectApprovalStep>();

            foreach (var group in groupedByStep)
            {
                // Selecciono la más especifica
                var selected = group
                    .OrderByDescending(r =>
                        (r.Area != null ? 1 : 0) + (r.Type != null ? 1 : 0)
                    )
                    .ThenBy(r => r.Id)
                    .FirstOrDefault();

                if (selected != null)
                {
                    selectedSteps.Add(new ProjectApprovalStep
                    {
                        ApproverRoleId = selected.ApproverRoleId.Value,
                        StepOrder = selected.StepOrder.Value,
                        Status = 1
                    });
                }
            }

            return selectedSteps;
        }


        public async Task<bool> UpdateStatus(updateApprovalStatusDto dto)
        {
            //Comprobaciones 
            if (dto.user == null || dto.step == null || dto.newStatus == null) { throw new BadRequestException("Datos de decisión inválidos"); }

            //Existe el status nuevo?
            var dbStatus = await _approvalStatusGetByIdHandler.Handle(new ApprovalStatusGetByIdQry(new ApprovalStatusDto() { Id = dto.newStatus.Value }));
            if (dbStatus == null) { throw new BadRequestException("El status ingresado no existe"); }

            //Existe el user?
            var dbUser = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = dto.user.Value }));
            if (dbUser == null) { throw new BadRequestException("El usuario ingresado no existe"); }

            //Existe el step?
            var dbStep = await _projectApprovalStepGetByIdHandler.Handle(new ProjectApprovalStepGetByIdQry(new ProjectApprovalStepDto() {Id = dto.step.Value }));
            if (dbStep == null || dbStep.Status == 2 || dbStep.Status == 3) { throw new BadRequestException("El step ingresado no existe"); }

            dbStep.Status = dto.newStatus.Value;
            dbStep.ApproverUserId = dto.user.Value;
            dbStep.Observations = dto.observation;
            dbStep.DecisionDate = DateTime.Now;

            

            var result = await _projectApprovalStepUpdateHander.Handle(new ProjectApprovalStepUpdateCmd(dbStep));

            //Si se cambió llamo al metodo para comprobar el status general
            if (result == true) { await UpdateGeneralProjectStatus(dbStep.ProjectProposalId); return true; }

            return false;
        }


        private async Task<bool> UpdateGeneralProjectStatus(Guid projectId)
        {
            var steps = await _projectApprovalStepGetAllHandler.Handle(new ProjectApprovalStepGetAllQry());

            if (steps.Count < 1) { throw new BadRequestException("No hay pasos cargados"); }

            steps = steps.Where(step => step.ProjectProposalId == projectId).ToList();

            var projectProposal = await _projectProposalGetByIdHandler.Handle(new ProjectProposalGetByIdQry(new ProjectProposalDto() { Id = projectId }));

            // Si alguno está rechazado
            if (steps.Any(step => step.Status == 3))
            {
                projectProposal.Status = 3;
                await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
                return false;
            }

            // Si todos están aprobados
            if (steps.All(step => step.Status == 2))
            {
                projectProposal.Status = 2;
                await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
                return true;
            }

            // Si alguno está observado
            if (steps.Any(step => step.Status == 4))
            {
                projectProposal.Status = 4;
                await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
                return false;
            }

            // Caso default: hay pasos pendientes
            projectProposal.Status = 1; // Pending
            await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
            return false;
        }


        public async Task<List<ProjectApprovalStep>> GetProjectSteps(Guid id)
        {
            if (id == null) { throw new Exception("Projecto inexistente"); }
            var results = new List<ProjectApprovalStep>();

            var projectSteps = await _projectApprovalStepGetAllHandler.Handle(new ProjectApprovalStepGetAllQry());
            if (projectSteps.Count > 0)
            {
                results = projectSteps
                .Where(step => step.ProjectProposalId == id).ToList();
            }
            else { throw new BadRequestException("El proyecto no tiene steps"); }

            return results;
        }

        public async Task<ProjectApprovalStepDto?> ViewNextProjectApprvalStep(Guid id)
        {
            var steps = await _projectApprovalStepGetAllHandler.Handle(new ProjectApprovalStepGetAllQry());

            if (steps == null || steps.Count == 0)
                return null;

            var filteredSteps = steps
                .Where(x => x.ProjectProposalId == id && (x.Status == 1 || x.Status == 4))
                .GroupBy(s => s.StepOrder)
                .Select(g => g.First())
                .ToList();

            if (filteredSteps.Count < 1)
                return null;

            var firstStep = filteredSteps.First();

            return new ProjectApprovalStepDto()
            {
                Id = firstStep.Id,
                StepOrder = firstStep.StepOrder,
                DecisionDate = firstStep.DecisionDate,
                Observations = firstStep.Observations,
                ProjectProposalId = firstStep.ProjectProposalId,
                ApproverUserId = firstStep.ApproverUserId,
                ApproverRoleId = firstStep.ApproverRoleId,
                Status = firstStep.Status
            };
        }

               

    }
}

