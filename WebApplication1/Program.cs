
using Application.ApprovalRules.Comands.Add;
using Application.ApprovalRules.Comands.Delete;
using Application.ApprovalRules.Comands.Update;
using Application.ApprovalRules.Queries.GetAll;
using Application.ApprovalRules.Queries.GetById;
using Application.ApprovalStatuses.Commands.Add;
using Application.ApprovalStatuses.Commands.Delete;
using Application.ApprovalStatuses.Commands.Update;
using Application.ApprovalStatuses.Queries.GetAll;
using Application.ApprovalStatuses.Queries.GetById;
using Application.ApproverRole.Commands.Add;
using Application.ApproverRole.Commands.Delete;
using Application.ApproverRole.Commands.Update;
using Application.ApproverRole.Queries.GetApproverRoles;
using Application.ApproverRole.Queries.GetById;
using Application.Areas.Commands.Add;
using Application.Areas.Commands.Delete;
using Application.Areas.Commands.Update;
using Application.Areas.Queries.GetAll;
using Application.Areas.Queries.GetById;
using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ProjectApprovalSteps.Commands.Add;
using Application.ProjectApprovalSteps.Commands.Delete;
using Application.ProjectApprovalSteps.Commands.Update;
using Application.ProjectApprovalSteps.Queries.GetAll;
using Application.ProjectApprovalSteps.Queries.GetById;
using Application.ProjectProposals.Commands.Add;
using Application.ProjectProposals.Commands.Delete;
using Application.ProjectProposals.Commands.Update;
using Application.ProjectProposals.Queries.GetAll;
using Application.ProjectProposals.Queries.GetById;
using Application.ProjectTypes.Commands.Add;
using Application.ProjectTypes.Commands.Delete;
using Application.ProjectTypes.Commands.Update;
using Application.ProjectTypes.Queries.GetAll;
using Application.ProjectTypes.Queries.GetById;
using Application.Services;
using Application.Users.Commands.Add;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Update;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.ApproverRole.Queries.GetALL;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProjectProposalService, ProjectProposalService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IProjectTypeService, ProjectTypeService>();
builder.Services.AddScoped<IApproverRoleService, ApproverRoleService>();
builder.Services.AddScoped<IApprovalStatusService, ApprovalStatusService>();
builder.Services.AddScoped<IUserService, UserService>();


///
var connectionString = builder.Configuration.GetConnectionString("LocalProjectConnection");

//con
builder.Services.AddDbContext<ProjectApprovalDbContext>(options =>
    options.UseSqlServer(connectionString));

#region

builder.Services.AddScoped<IApprovalRuleRepository, ApprovalRuleRepository>();
builder.Services.AddScoped<IApprovalStatusRepository, ApprovalStatusRepository>();
builder.Services.AddScoped<IApproverRoleRepository, ApproverRoleRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IProjectApprovalStepRepository, ProjectApprovalStepRepository>();
builder.Services.AddScoped<IProjectProposalRepository, ProjectProposalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();

// Command-Query Handlers
builder.Services.AddScoped<UserGetAllQryHndlr>();
builder.Services.AddScoped<UserGetByIdQryHndlr>();
builder.Services.AddScoped<UserAddCmdHndlr>();
builder.Services.AddScoped<UserDeleteCmdHndlr>();
builder.Services.AddScoped<UserUpdateCmdHndlr>();

builder.Services.AddScoped<AreaGetAllQryHndlr>();
builder.Services.AddScoped<AreaGetByIdQryHndlr>();
builder.Services.AddScoped<AreaAddCmdHndlr>();
builder.Services.AddScoped<AreaDeleteCmdHndlr>();
builder.Services.AddScoped<AreaUpdateCmdHndlr>();

builder.Services.AddScoped<ApproverRoleGetAllQryHndlr>();
builder.Services.AddScoped<ApproverRoleGetByIdQryHndlr>();
builder.Services.AddScoped<ApproverRoleAddCmdHndlr>();
builder.Services.AddScoped<ApproverRoleDeleteCmdHndlr>();
builder.Services.AddScoped<ApproverRoleUpdateCmdHndlr>();

builder.Services.AddScoped<ProjectTypeGetAllQryHndlr>();
builder.Services.AddScoped<ProjectTypeGetByIdQryHndlr>();
builder.Services.AddScoped<ProjectTypeAddCmdHndlr>();
builder.Services.AddScoped<ProjectTypeDeleteCmdHndlr>();
builder.Services.AddScoped<ProjectTypeUpdateCmdHndlr>();

builder.Services.AddScoped<ApprovalStatusGetAllQryHndlr>();
builder.Services.AddScoped<ApprovalStatusGetByIdQryHndlr>();
builder.Services.AddScoped<ApprovalStatusAddCmdHndlr>();
builder.Services.AddScoped<ApprovalStatusDeleteCmdHndlr>();
builder.Services.AddScoped<ApprovalStatusUpdateCmdHndlr>();

builder.Services.AddScoped<ApprovalRuleAddCmdHndlr>();
builder.Services.AddScoped<ApprovalRuleDeleteCmdHndlr>();
builder.Services.AddScoped<ApprovalRuleUpdateCmdHndlr>();
builder.Services.AddScoped<ApprovalRuleGetAllQryHndlr>();
builder.Services.AddScoped<ApprovalRuleGetByIdQryHndlr>();

builder.Services.AddScoped<ProjectProposalAddCmdHndlr>();
builder.Services.AddScoped<ProjectProposalDeleteCmdHndlr>();
builder.Services.AddScoped<ProjectProposalUpdateCmdHndlr>();
builder.Services.AddScoped<ProjectProposalGetAllQryHndlr>();
builder.Services.AddScoped<ProjectProposalGetByIdQryHndlr>();

builder.Services.AddScoped<ProjectApprovalStepAddCmdHndlr>();
builder.Services.AddScoped<ProjectApprovalStepDeleteCmdHndlr>();
builder.Services.AddScoped<ProjectApprovalStepUpdateCmdHndlr>();
builder.Services.AddScoped<ProjectApprovalStepGetAllQryHndlr>();
builder.Services.AddScoped<ProjectApprovalStepGetByIdQryHndlr>();

// Services
builder.Services.AddScoped<IProjectApprovalStepService,ProjectApprovalStepService>();
builder.Services.AddScoped<IApproverRoleService, ApproverRoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IProjectTypeService, ProjectTypeService>();
builder.Services.AddScoped<IProjectProposalService, ProjectProposalService>();

// CQRS - Command & Query interfaces

//USERSERVICE
builder.Services.AddScoped<IQueryHandler<UserGetByIdQry, User?>, UserGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<UserGetAllQry, List<UserDto>>, UserGetAllQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ApproverRoleGetByIdQry, ApproverRole?>, ApproverRoleGetByIdQryHndlr>();

//TYPESERVICE
builder.Services.AddScoped<IQueryHandler<ProjectTypeGetAllQry, List<ProjectType>>, ProjectTypeGetAllQryHndlr>();

//PROJECTPROPOSALSERVICE
builder.Services.AddScoped<IQueryHandler<AreaGetByIdQry, Area?>, AreaGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ProjectTypeGetByIdQry, ProjectType?>, ProjectTypeGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatus?>, ApprovalStatusGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<UserGetByIdQry, User?>, UserGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ProjectProposalGetByIdQry, ProjectProposal?>, ProjectProposalGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?>, ApproverRoleGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ProjectProposalGetAllQry, List<ProjectProposalDto>>, ProjectProposalGetAllQryHndlr>();

builder.Services.AddScoped<ICommandHandler<ProjectProposalAddCmd, bool>, ProjectProposalAddCmdHndlr>();
builder.Services.AddScoped<ICommandHandler<ProjectProposalUpdateCmd, bool>, ProjectProposalUpdateCmdHndlr>();

//PROJECTAPPROVALSTEPSERVICE
builder.Services.AddScoped<IQueryHandler<ApprovalRuleGetAllQry, List<ApprovalRuleDto>>, ApprovalRuleGetAllQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ProjectApprovalStepGetAllQry, List<ProjectApprovalStep>>, ProjectApprovalStepGetAllQryHndlr>();
builder.Services.AddScoped<IQueryHandler<UserGetByIdQry, User?>, UserGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatus?>, ApprovalStatusGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ProjectApprovalStepGetByIdQry, ProjectApprovalStep?>, ProjectApprovalStepGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ProjectProposalGetByIdQry, ProjectProposal?>, ProjectProposalGetByIdQryHndlr>();

builder.Services.AddScoped<ICommandHandler<ProjectApprovalStepUpdateCmd, bool>, ProjectApprovalStepUpdateCmdHndlr>();
builder.Services.AddScoped<ICommandHandler<ProjectProposalUpdateCmd, bool>, ProjectProposalUpdateCmdHndlr>();

//AREASERVICE
builder.Services.AddScoped<IQueryHandler<AreaGetAllQry, List<AreaDto>>, AreaGetAllQryHndlr>();

//APPROVERROLESERVICE
builder.Services.AddScoped<IQueryHandler<ApproverRoleGetByIdQry, ApproverRole?>, ApproverRoleGetByIdQryHndlr>();
builder.Services.AddScoped<IQueryHandler<ApproverRoleGetAllQry, List<ApproverRoleDto>>, ApproverRoleGetAllQryHndlr>();

//APPROVALSTATUS
builder.Services.AddScoped<IQueryHandler<ApprovalStatusGetAllQry, List<ApprovalStatusDto>>, ApprovalStatusGetAllQryHndlr>();

#endregion
///
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
});


//Prevenir las respuestas automaticas del modelstate
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// 1. Definir política CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5501")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();


//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();



app.Run();
