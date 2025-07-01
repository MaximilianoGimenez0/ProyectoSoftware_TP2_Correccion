using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApprovalStatuses.Queries.GetAll;
using Application.ApprovalStatuses.Queries.GetById;
using Application.ApproverRole.Queries.GetById;
using Application.Areas.Queries.GetAll;
using Application.Areas.Queries.GetById;
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ProjectApprovalSteps.Commands.Update;
using Application.ProjectApprovalSteps.Queries.GetById;
using Application.ProjectProposals.Commands.Add;
using Application.ProjectProposals.Commands.Update;
using Application.ProjectProposals.Queries.GetAll;
using Application.ProjectProposals.Queries.GetById;
using Application.ProjectTypes.Queries.GetById;
using Application.Users.Queries.GetById;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProjectProposalService : IProjectProposalService
    {
        //Queries
        private readonly IQueryHandler<AreaGetByIdQry, Area?> _areaGetByIdHandler;
        private readonly IQueryHandler<ProjectTypeGetByIdQry, ProjectType?> _projectTypeGetByIdHandler;
        private readonly IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatus?> _approvalStatusGetByIdHandler;
        private readonly IQueryHandler<UserGetByIdQry, User?> _userGetByIdHandler;
        private readonly IQueryHandler<ProjectProposalGetByIdQry, ProjectProposal?> _projectProposalGetByIdHandler;
        private readonly IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?> _approverRoleGetByIdHandler;
        private readonly IQueryHandler<ProjectApprovalStepGetByIdQry, ProjectApprovalStep?> _projectApprovalStepGetByIdHandler;

        private readonly IQueryHandler<ProjectProposalGetAllQry, List<ProjectProposalDto>> _projectProposalGetAllHandler;
        
        private readonly ICommandHandler<ProjectProposalUpdateCmd, bool> _projectProposalUpdateHandler;
        private readonly ICommandHandler<ProjectProposalAddCmd,bool> _projectProposalAddHandler;
        private readonly ICommandHandler<ProjectApprovalStepUpdateCmd,bool> _projectApprovalStepUpdateHandler;
        
        private readonly IProjectApprovalStepService _projectApprovalStepService;

        public ProjectProposalService
            (
            IQueryHandler<AreaGetByIdQry, Area?> areaGetByIdHandler,
            ICommandHandler<ProjectProposalAddCmd, bool> projectProposalAddHandler,
            IQueryHandler<ProjectTypeGetByIdQry, ProjectType?> projectTypeGetByIdHandler,
            IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatus?> approvalStatusGetByIdHandler,
            IQueryHandler<UserGetByIdQry, User?> userGetByIdHandler,
            IQueryHandler<ProjectProposalGetAllQry, List<ProjectProposalDto>> projectProposalGetAllHandler,
            IQueryHandler<ProjectProposalGetByIdQry, ProjectProposal?> projectProposalGetByIdHandler,
            IProjectApprovalStepService projectApprovalStepService,
            IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?> approverRoleGetByIdHandler,
            ICommandHandler<ProjectProposalUpdateCmd, bool> projectProposalUpdateHandler,
            ICommandHandler<ProjectApprovalStepUpdateCmd, bool> projectApprovalStepUpdateHandler,
            IQueryHandler<ProjectApprovalStepGetByIdQry, ProjectApprovalStep?> projectApprovalStepGetByIdHandler)
        {
            _projectProposalGetAllHandler = projectProposalGetAllHandler;
            _areaGetByIdHandler = areaGetByIdHandler;
            _projectProposalAddHandler = projectProposalAddHandler;
            _projectTypeGetByIdHandler = projectTypeGetByIdHandler;
            _approvalStatusGetByIdHandler = approvalStatusGetByIdHandler;
            _userGetByIdHandler = userGetByIdHandler;
            _projectProposalGetByIdHandler = projectProposalGetByIdHandler;
            _projectApprovalStepService = projectApprovalStepService;
            _approverRoleGetByIdHandler = approverRoleGetByIdHandler;
            _projectProposalUpdateHandler = projectProposalUpdateHandler;
            _projectApprovalStepUpdateHandler = projectApprovalStepUpdateHandler;
            _projectApprovalStepGetByIdHandler = projectApprovalStepGetByIdHandler;
        }

        public async Task<ProjectResponse> GetCompleteProjectGetById(Guid id)
        {
            if (id == Guid.Empty) { throw new BadRequestException("Id nulo"); }
            var project = await _projectProposalGetByIdHandler.Handle(new ProjectProposalGetByIdQry(new ProjectProposalDto() { Id = id }));
            if (project == null) { throw new NotFoundException("El proyecto no se encuentra registrado"); }

            // GetStatus
            var statusId = project.Status;
            var status = await _approvalStatusGetByIdHandler.Handle(new ApprovalStatusGetByIdQry(new ApprovalStatusDto() { Id = statusId }));
            var projectStatus = new GenericResponse() { id = status.Id, name = status.Name };

            // GetArea
            var areaId = project.Area;
            var area = await _areaGetByIdHandler.Handle(new AreaGetByIdQry(new AreaDto() { Id = areaId }));

            var projectArea = new GenericResponse() { id = area.Id, name = area.Name };

            // GetType
            var typeId = project.Type;
            var type = await _projectTypeGetByIdHandler.Handle(new ProjectTypeGetByIdQry(new ProjectTypeDto() { Id = typeId }));

            var projectType = new GenericResponse() { id = type.Id, name = type.Name };

            // GetUser
            var userId = project.CreateBy;
            var user = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = userId }));
            if (user == null)
            {
                throw new NotFoundException("Usuario de proyecto inexistente");
            }

            var projectUserRole = await _approverRoleGetByIdHandler.Handle(new ApproverRoleGetByIdQry(new ApproverRoleDto() { Id = user.Role }));

            var projectUser = new UsersResponse()
            {
                id = user.Id,
                name = string.IsNullOrWhiteSpace(user.Name) ? "Sin nombre" : user.Name,
                email = string.IsNullOrWhiteSpace(user.Email) ? "Sin email" : user.Email,
                role = new GenericResponse() { id = projectUserRole.Id, name = string.IsNullOrWhiteSpace(projectUserRole.Name) ? "Sin rol" : projectUserRole.Name }
            };

            var steps = await _projectApprovalStepService.GetProjectSteps(id);

            var projectSteps = new List<ApprovalStepResponse>();

            foreach (var step in steps)
            {
                Dtos.Responses.UsersResponse tempStepUser;

                if (step.ApproverUserId != null)
                {
                    var _tempStepUser = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = step.ApproverUserId.Value }));
                    var _tempStepUserRole = await _approverRoleGetByIdHandler.Handle(new ApproverRoleGetByIdQry(new ApproverRoleDto() { Id = _tempStepUser?.Role ?? 0 }));

                    tempStepUser = new Dtos.Responses.UsersResponse()
                    {
                        id = _tempStepUser?.Id ?? 0,
                        name = string.IsNullOrWhiteSpace(_tempStepUser?.Name) ? "Sin nombre" : _tempStepUser.Name,
                        email = string.IsNullOrWhiteSpace(_tempStepUser?.Email) ? "Sin email" : _tempStepUser.Email,
                        role = new GenericResponse()
                        {
                            id = _tempStepUserRole?.Id ?? 0,
                            name = string.IsNullOrWhiteSpace(_tempStepUserRole?.Name) ? "Sin rol" : _tempStepUserRole.Name
                        }
                    };
                }
                else
                {
                    // Si no hay usuario asignado, asigno valores por defecto que no generen error
                    tempStepUser = new Dtos.Responses.UsersResponse()
                    {
                        id = 0,
                        name = "Sin usuario asignado",
                        email = "Sin email",
                        role = new GenericResponse()
                        {
                            id = 0,
                            name = "Sin rol"
                        }
                    };
                }

                var _tempStepApproverRole = await _approverRoleGetByIdHandler.Handle(new ApproverRoleGetByIdQry(new ApproverRoleDto() { Id = step.ApproverRoleId }));
                var tempStepApproverRole = new GenericResponse()
                {
                    id = _tempStepApproverRole.Id,
                    name = string.IsNullOrWhiteSpace(_tempStepApproverRole.Name) ? "Sin rol" : _tempStepApproverRole.Name
                };

                var _tempStepStatus = await _approvalStatusGetByIdHandler.Handle(new ApprovalStatusGetByIdQry(new ApprovalStatusDto() { Id = step.Status }));
                var tempStepStatus = new GenericResponse()
                {
                    id = _tempStepStatus.Id,
                    name = string.IsNullOrWhiteSpace(_tempStepStatus.Name) ? "Sin estado" : _tempStepStatus.Name
                };

                var tempStep = new ApprovalStepResponse()
                {
                    id = step.Id,
                    stepOrder = step.StepOrder,
                    decisionDate = step.DecisionDate,
                    observations = step.Observations ?? string.Empty,
                    approverUser = tempStepUser,
                    approverRole = tempStepApproverRole,
                    status = tempStepStatus
                };
                projectSteps.Add(tempStep);
            }

            var Response = new ProjectResponse()
            {
                id = project.Id,
                title = project.Title ?? string.Empty,
                description = project.Description ?? string.Empty,
                amount = project.EstimatedAmount,
                duration = project.EstimatedDuration,
                area = projectArea,
                status = projectStatus,
                type = projectType,
                user = projectUser,
                steps = projectSteps
            };

            return Response;
        }



        public async Task<ProjectResponse> present(ProjectProposalDto project)
        {
            
            var requiredFields = new object?[]
            {
                project.Title,
                project.Description,
                project.EstimatedAmount,
                project.EstimatedDuration,
                project.Area,
                project.Type,
                project.CreateBy
            };

            if (requiredFields.Any(f => f == null))
            {
                throw new BadRequestException("Datos del proyecto inválidos");
            }



            //El proyecto no puede tener el mismo titulo que otro proyecto ya creado
            var projects = await _projectProposalGetAllHandler.Handle(new ProjectProposalGetAllQry());
            projects = projects.Where(c => c.Title == project.Title).ToList();

            
            if (projects.Count > 0)
            {
                throw new BadRequestException("Ya existe un proyecto con el mismo título");
            }

            //GetStatus
            var status = await _approvalStatusGetByIdHandler.Handle(new ApprovalStatusGetByIdQry(new ApprovalStatusDto() { Id = 1 }));
            var projectStatus = new GenericResponse() { id = status.Id, name = status.Name};


            //Comprobación de fk con Area
            var areaId = project.Area.Value;
            var area = await _areaGetByIdHandler.Handle(new AreaGetByIdQry(new AreaDto() { Id = areaId }));
            if (area == null) 
            {
                throw new BadRequestException("El area ingresada no existe");
            }

            //Comprobación de fk con Type
            var typeId = project.Type.Value;
            var type = await _projectTypeGetByIdHandler.Handle(new ProjectTypeGetByIdQry(new ProjectTypeDto() { Id = typeId }));
            if (type == null) 
            {
                throw new BadRequestException("El tipo ingresado no existe");
            }


            //Comprobación con fk de User
            var userId = project.CreateBy.Value;
            var user = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = userId }));
            if (user == null) 
            {
                throw new BadRequestException("El usuario ingresado no existe");
            }                   

            //Validaciones finales
            var esValido = ProjectProposalValidator.Validar(project);
            if (esValido.EsValido == false)
            {
                throw new BadRequestException(esValido.Mensaje);
            }

            var id = Guid.NewGuid();

            //Mapeo de dto a clase de dominio
            var DtoProposal = new ProjectProposal()
            {
                Id = id,
                Title = project.Title,
                Description = project.Description,
                EstimatedAmount = project.EstimatedAmount.Value,
                EstimatedDuration = project.EstimatedDuration.Value,
                CreateAt = DateTime.Now,
                Status = 1,
                Area = project.Area.Value,
                Type = project.Type.Value,
                CreateBy = project.CreateBy.Value
            };


            var calculatedSteps = await _projectApprovalStepService.CalculateSteps(DtoProposal);

            //Se asignan los pasos calculados al proyecto
            DtoProposal.Steps = calculatedSteps;

            //Se establece la relación del lado del paso hacia el proyecto
            foreach (var step in calculatedSteps) {
                step.StepProjectProposal = DtoProposal;
            }

            //Se maneja la creación de forma transaccional
            await _projectProposalAddHandler.Handle(new ProjectProposalAddCmd(DtoProposal));

            return await GetCompleteProjectGetById(id);
            
        }

        public async Task<List<ProjectProposalViewDto>> ViewAll()
        {
            var dbProjects = await _projectProposalGetAllHandler.Handle(new ProjectProposalGetAllQry());
            var dtoProjects = new List<ProjectProposalViewDto>();

            foreach (var project in dbProjects)
            {
                var user = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = project.CreateBy }));
                var type = await _projectTypeGetByIdHandler.Handle(new ProjectTypeGetByIdQry(new ProjectTypeDto() { Id = project.Type }));
                var status = await _approvalStatusGetByIdHandler.Handle(new ApprovalStatusGetByIdQry(new ApprovalStatusDto() { Id= project.Status }));
                var area = await _areaGetByIdHandler.Handle(new AreaGetByIdQry(new AreaDto() { Id = project.Area }));

                var temp = new ProjectProposalViewDto()
                {
                    Id = project.Id,
                    Title = project.Title == null ? "Titulo vacio" : project.Title,
                    EstimatedAmount = project.EstimatedAmount,
                    EstimatedDuration = project.EstimatedDuration,
                    CreateAt = project.CreateAt,
                    Area = area.Name,
                    Type = type.Name,
                    Status = status.Name,
                    CreateBy = user.Name

                };

                dtoProjects.Add(temp);

            }

            return dtoProjects;
        }

        public async Task<List<ProjectShortResponse>> GetFiltered(string? title, int? status, int? createBy, int? approvalUser)
        {
                       
            var projects = await _projectProposalGetAllHandler.Handle(new ProjectProposalGetAllQry());
            
            if (projects.Count<0) { throw new BadRequestException("Parámetro de consulta inválido"); }

            //Filtrado por usuario que puede aprobar la proxima solicitud del proyecto
            if (approvalUser != null) 
            {
                var _approvalUser = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() {Id = approvalUser}));
                if (_approvalUser == null) { throw new BadRequestException("El usuario no existe"); }

                var approverRole = _approvalUser.Role;

                var temp = new List<ProjectProposalDto>();
                foreach (var project in projects) 
                {
                    var _nextStep = await _projectApprovalStepService.ViewNextProjectApprvalStep(project.Id.Value);
                    if (_nextStep != null && _nextStep.ApproverRoleId == approverRole) { temp.Add(project); }
                }

                projects = temp;
            }

            //Filtrado por titulo de proyecto
            if (title != null)
            {
                projects = projects.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            }

            //Filtrado por status de proyecto
            if (status != null) 
            {
                projects = projects.Where(x => x.Status == status).ToList();
            }

            //Filtrado por creador
            if (createBy != null) 
            {
                projects = projects.Where(x => x.CreateBy == createBy).ToList();
            }

            
            var results = new List<ProjectShortResponse>();
            foreach (var project in projects) 
            {
                //area
                var _projectArea = await _areaGetByIdHandler.Handle(new AreaGetByIdQry(new AreaDto() { Id = project.Status}));
                
                //status
                var _projectStatus = await _approvalStatusGetByIdHandler.Handle( new ApprovalStatusGetByIdQry(new ApprovalStatusDto() { Id = project.Status }));
                
                //type
                var _projectType = await _projectTypeGetByIdHandler.Handle(new ProjectTypeGetByIdQry(new ProjectTypeDto() { Id = project.Type }));
                
                var temp = new ProjectShortResponse() 
                {
                    id = project.Id.Value,
                    title = project.Title,
                    description = project.Description,
                    amount = project.EstimatedAmount.Value,
                    duration = project.EstimatedDuration.Value,
                    area = _projectArea.Name,
                    status = _projectStatus.Name,
                    type = _projectType.Name,
                };
                results.Add(temp);
                
            }

            return results;

        }

        public async Task<ProjectResponse> UpdateProject(Guid id, ProjectUpdateRequest update)
        {
            //El proyecto no pu ede tener el mismo titulo que otro proyecto ya creado
            var project = await _projectProposalGetByIdHandler.Handle(new ProjectProposalGetByIdQry(new ProjectProposalDto() { Id = id}));

            if (project == null) { throw new BadRequestException("El proyecto no se encuentra"); }

            if (update.title != null && project.Title != update.title) { 
            
            var projects = await _projectProposalGetAllHandler.Handle(new ProjectProposalGetAllQry());
            projects = projects.Where(c => c.Title == update.title).ToList();

            if (projects.Count > 0) { throw new BadRequestException("Ya existe un proyecto con el mismo titulo"); }
                        
            }
                                   
            
            if (project.Status != 4) { throw new BadRequestException("El proyecto se encuentra en un estado que no permite modificaciones"); }

            if (update.title != null) { project.Title = update.title; }
            if (update.description != null) { project.Description = update.description; }
            if (update.duration != null) { project.EstimatedDuration = (int)update.duration; }
          
            await _projectProposalUpdateHandler.Handle(new ProjectProposalUpdateCmd(project));

            var result = await GetCompleteProjectGetById(project.Id);

            return result;
        }



        public async Task<ProjectResponse> UpdateProjectStep(Guid projectId, updateApprovalStatusDto dto)
        {
            
            var project = await _projectProposalGetByIdHandler.Handle(new ProjectProposalGetByIdQry(new ProjectProposalDto() { Id = projectId }));
            if (project == null) { throw new BadRequestException("El proyecto ingresado no existe"); }

            
            if (project.Status == 2 || project.Status == 3) { throw new ConflictException("El proyecto ya no se encuentra en un estado que permite modificaciones"); } 
               
            
            var user = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = dto.user }));
            if (user == null) { throw new BadRequestException("El usuario ingresado no existe"); }
                

            var nextStep = await _projectApprovalStepService.ViewNextProjectApprvalStep(projectId);
            if (nextStep == null) { throw new BadRequestException("El proyecto no tiene un paso pendiente de aprobación"); }


            if (nextStep.Id != dto.step) { throw new BadRequestException("El id del paso no corresponde con el próximo paso pendiente de aprobación"); }


            if (nextStep.ApproverRoleId.Value != user.Role) { throw new BadRequestException("El rol del usuario no corresponde, no puede aprobar este paso"); }

            var dbStep = await _projectApprovalStepGetByIdHandler.Handle(new ProjectApprovalStepGetByIdQry(new ProjectApprovalStepDto() {Id = dto.step }));

            if (dbStep == null) { throw new BadRequestException("El paso no existe"); }

            dbStep.Status = dto.newStatus.Value;
            dbStep.DecisionDate = DateTime.Now;
            dbStep.ApproverUserId = dto.user.Value;
            dbStep.Observations = dto.observation;
            
            var updateSuccess = await _projectApprovalStepUpdateHandler.Handle(new ProjectApprovalStepUpdateCmd(dbStep));

            if (!updateSuccess) { throw new BadRequestException("Datos de decisión inválidos"); }


            project = await _projectProposalGetByIdHandler.Handle(new ProjectProposalGetByIdQry(new ProjectProposalDto() {Id = projectId }));

            if (project.Steps.Any(s => s.Status == 3))       // Algún paso rechazado
            {
                project.Status = 3;
            }
            else if (project.Steps.All(s => s.Status == 2))  // Todos aprobados
            {
                project.Status = 2;
            }
            else if (project.Steps.Any(s => s.Status == 4))  // Alguno observado
            {
                project.Status = 4;
            }
            else                                            // Todavía hay pendientes
            {
                project.Status = 1;
            }


            var updatedProject = await _projectProposalUpdateHandler.Handle(new ProjectProposalUpdateCmd(project));
            if (!updatedProject) { throw new BadRequestException("El proyecto no se pudo actualizar"); }

            return await GetCompleteProjectGetById(projectId);
        }






    }
}
