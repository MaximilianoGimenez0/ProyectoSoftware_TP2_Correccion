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

        /*
        public async Task<List<ProjectApprovalStepDto>> ViewPendingWithRole(UserDto user)
        {
            var pendingSteps = new List<ProjectApprovalStepDto>();

            var role = user.Role;
            if (user.Id == null || user.Role == null) { return pendingSteps; }

            var steps = await _projectApprovalStepGetAllHandler.Handle(new ProjectApprovalStepGetAllQry());

            var filtrado = steps
                .Where(s => s.Status == 1 & s.ApproverRoleId == role)
                .GroupBy(s => new { s.StepOrder, s.ProjectProposalId })
                .Select(g => g.First())
                .ToList();

            return filtrado;
        }
        */

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
                
        public async Task<List<Domain.Entities.ProjectApprovalStep>> CalculateSteps(Domain.Entities.ProjectProposal project)
        {
            var rules = await _approvalRuleGetAllHandler.Handle(new ApprovalRuleGetAllQry());

            var reglasAgrupadas = rules.GroupBy(r => r.StepOrder).ToList();
            
            var stepEvaluationResults = new List<StepEvaluationResult>();

            foreach (var group in reglasAgrupadas)
            {

                int stepOrder = group.First().StepOrder.Value;
                StepEvaluationResult bestMatch = null;
                int bestScore = -1;

                foreach (var rule in group)
                {
                    int tempScore = 1;
                    //Comprobar: 
                    // Se encuentra dentro de los valores de amount -> Excluyente
                    if (project.EstimatedAmount > rule.MaxAmount || project.EstimatedAmount < rule.MinAmount) { continue; }

                    // Si la regla tiene area definida y mi proyecto tiene un area diferente no vale --> Excluyente
                    // Si la regla tiene area definida y es igual al de mi proyecto --> SUMA PUNTO

                    if (rule.Area != null && project.Area != rule.Area ) { continue; }
                    // Si la regla tiene un tipo definido y mi proyecto tiene un tipo diferente no vale --> Excluyente
                    // Si la regla tiene tipo definido y es igual al de mi proyecto --> SUMA PUNTO
                    if (rule.Area == project.Area) 
                    {
                        //SUMA PUNTO
                        tempScore++;
                    }

                    if (rule.Type != null && project.Type != rule.Type ) { continue; }
                    if (rule.Type == project.Type)
                    {
                        //SUMA PUNTO
                        tempScore++;
                    }


                    // Si la regla analizada tiene un score > bestScore --> Creo una nueva regla y seteo el bestScore a mi score
                    // DEFINO EL BESTMATCH CON LOS DATOS DE LA REGLA 
                    // SI EL SCORE ES EL MAXIMO ENTONCES SALGO DEL LOOP DEL FOREACH PORQUE YA ENCONTRÉ LO QUE QUERIA
                    if (tempScore > bestScore) 
                    {
                        bestMatch = new StepEvaluationResult() 
                        {
                            RoleId = rule.ApproverRoleId.Value,
                            StepOrder = stepOrder,
                            Score = tempScore,
                            RuleId = rule.Id.Value
                        };
                        if (tempScore == 3) { break; }
                    }
                }

                if (bestMatch != null)
                {
                    stepEvaluationResults.Add(bestMatch);
                }
            }

            var calculatedSteps = new List<Domain.Entities.ProjectApprovalStep>();
            foreach (var step in stepEvaluationResults)
            {
                calculatedSteps.Add(new Domain.Entities.ProjectApprovalStep()
                {
                    StepOrder = step.StepOrder,
                    ApproverRoleId = step.RoleId,
                    Status = 1
                });
            }

            return calculatedSteps;
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

        //Chequeo si todos los pasos del proyecto están aprobados o si alguno fue rechazado para modificar el status general
        private async Task<bool> UpdateGeneralProjectStatus(Guid projectId)
        {
            var steps = await _projectApprovalStepGetAllHandler.Handle(new ProjectApprovalStepGetAllQry());

            if (steps.Count < 1) { throw new BadRequestException("No hay pasos cargados"); }

            steps = steps.Where(step => step.ProjectProposalId == projectId).ToList();

            var projectProposal = await _projectProposalGetByIdHandler.Handle(new ProjectProposalGetByIdQry(new ProjectProposalDto() { Id = projectId }));

            // Si alguno tiene status 3 (RECHAZADO), retorno false
            if (steps.Any(step => step.Status == 3))
            {
                projectProposal.Status = 3;
                await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
                return false;
            }

            // Si todos tienen status 2 (APROBADO)
            if (steps.All(step => step.Status == 2))
            {
                projectProposal.Status = 2;
                await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
                return true;
            }

            // Si alguno tiene status 3 (RECHAZADO), retorno false
            if (steps.Any(step => step.Status == 4))
            {
                projectProposal.Status = 4;
                await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
                return false;
            }

            //projectProposal.Status = 4;
            //await _projectProposalUpdateHander.Handle(new ProjectProposalUpdateCmd(projectProposal));
            return true;

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


        // Clase interna para almacenar los resultados de la evaluación de cada paso
        public class StepEvaluationResult
        {
            public long RuleId { get; set; }
            public int StepOrder { get; set; }
            public int Score { get; set; }
            public int RoleId { get; set; }
        }

    }
}

