// See https://aka.ms/new-console-template for more information
using System;
using Application.ApproverRole.Commands.Add;
using Application.ApproverRole.Commands.Delete;
using Application.ApproverRole.Commands.Update;
using Application.ApproverRole.Queries.GetALL;
using Application.ApproverRole.Queries.GetApproverRoles;
using Application.ApproverRole.Queries.GetById;
using Application.Areas.Commands.Add;
using Application.Areas.Commands.Delete;
using Application.Areas.Commands.Update;
using Application.Areas.Queries.GetAll;
using Application.Areas.Queries.GetById;
using Application.Dtos;
using Application.Users.Commands.Add;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Update;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Formats.Asn1.AsnWriter;
using Application.ProjectTypes.Queries.GetAll;
using Application.ProjectTypes.Queries.GetById;
using Application.ProjectTypes.Commands.Add;
using Application.ProjectTypes.Commands.Delete;
using Application.ProjectTypes.Commands.Update;
using Application.ApprovalStatuses.Queries.GetAll;
using Application.ApprovalStatuses.Queries.GetById;
using Application.ApprovalStatuses.Commands.Add;
using Application.ApprovalStatuses.Commands.Delete;
using Application.ApprovalStatuses.Commands.Update;
using Application.ApprovalRules.Comands.Add;
using Application.ApprovalRules.Comands.Delete;
using Application.ApprovalRules.Comands.Update;
using Application.ApprovalRules.Queries.GetAll;
using Application.ApprovalRules.Queries.GetById;
using Application.ProjectProposals.Commands.Add;
using Application.ProjectProposals.Commands.Delete;
using ProjectApproval.ConsoleInteractions.Menues;
using Application.Services;
using ProjectApproval.ConsoleInteractions.Inputs;
using static Application.Services.ProjectApprovalStepService;
using Application.ProjectProposals.Commands.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Application.ProjectProposals.Queries.GetAll;
using Application.ProjectProposals.Queries.GetById;
using Application.ProjectApprovalSteps.Commands.Add;
using Application.ProjectApprovalSteps.Commands.Delete;
using Application.ProjectApprovalSteps.Commands.Update;
using Application.ProjectApprovalSteps.Queries.GetAll;
using Application.ProjectApprovalSteps.Queries.GetById;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProjectApproval.FirstSetupBuild;
using Application.Interfaces;
using Application.Interfaces.Services;
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("LocalProjectConnection");

        //con
        services.AddDbContext<ProjectApprovalDbContext>(options =>
            options.UseSqlServer(connectionString));



        //repositories

        services.AddScoped<IApprovalRuleRepository, ApprovalRuleRepository>();
        services.AddScoped<IApprovalStatusRepository, ApprovalStatusRepository>();
        services.AddScoped<IApproverRoleRepository, ApproverRoleRepository>();
        services.AddScoped<IAreaRepository, AreaRepository>();
        services.AddScoped<IProjectApprovalStepRepository, ProjectApprovalStepRepository>();
        services.AddScoped<IProjectProposalRepository, ProjectProposalRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();


        //Command-Query handlers

        services.AddScoped<UserGetAllQryHndlr>();
        services.AddScoped<UserGetByIdQryHndlr>();
        services.AddScoped<UserAddCmdHndlr>();
        services.AddScoped<UserDeleteCmdHndlr>();
        services.AddScoped<UserUpdateCmdHndlr>();

        services.AddScoped<AreaGetAllQryHndlr>();
        services.AddScoped<AreaGetByIdQryHndlr>();
        services.AddScoped<AreaAddCmdHndlr>();
        services.AddScoped<AreaDeleteCmdHndlr>();
        services.AddScoped<AreaUpdateCmdHndlr>();

        services.AddScoped<ApproverRoleGetAllQryHndlr>();
        services.AddScoped<ApproverRoleGetByIdQryHndlr>();
        services.AddScoped<ApproverRoleAddCmdHndlr>();
        services.AddScoped<ApproverRoleDeleteCmdHndlr>();
        services.AddScoped<ApproverRoleUpdateCmdHndlr>();

        services.AddScoped<ProjectTypeGetAllQryHndlr>();
        services.AddScoped<ProjectTypeGetByIdQryHndlr>();
        services.AddScoped<ProjectTypeAddCmdHndlr>();
        services.AddScoped<ProjectTypeDeleteCmdHndlr>();
        services.AddScoped<ProjectTypeUpdateCmdHndlr>();

        services.AddScoped<ApprovalStatusGetAllQryHndlr>();
        services.AddScoped<ApprovalStatusGetByIdQryHndlr>();
        services.AddScoped<ApprovalStatusAddCmdHndlr>();
        services.AddScoped<ApprovalStatusDeleteCmdHndlr>();
        services.AddScoped<ApprovalStatusUpdateCmdHndlr>();

        services.AddScoped<ApprovalRuleAddCmdHndlr>();
        services.AddScoped<ApprovalRuleDeleteCmdHndlr>();
        services.AddScoped<ApprovalRuleUpdateCmdHndlr>();
        services.AddScoped<ApprovalRuleGetAllQryHndlr>();
        services.AddScoped<ApprovalRuleGetByIdQryHndlr>();

        services.AddScoped<ProjectProposalAddCmdHndlr>();
        services.AddScoped<ProjectProposalDeleteCmdHndlr>();
        services.AddScoped<ProjectProposalUpdateCmdHndlr>();
        services.AddScoped<ProjectProposalGetAllQryHndlr>();
        services.AddScoped<ProjectProposalGetByIdQryHndlr>();

        services.AddScoped<ProjectApprovalStepAddCmdHndlr>();
        services.AddScoped<ProjectApprovalStepDeleteCmdHndlr>();
        services.AddScoped<ProjectApprovalStepUpdateCmdHndlr>();
        services.AddScoped<ProjectApprovalStepGetAllQryHndlr>();
        services.AddScoped<ProjectApprovalStepGetByIdQryHndlr>();


       

        services.AddScoped<ProjectApprovalStepService>();

        ///
        services.AddScoped<IQueryHandler<ProjectApprovalStepGetAllQry, List<ProjectApprovalStepDto>>, ProjectApprovalStepGetAllQryHndlr>();
        services.AddScoped<IQueryHandler<ApprovalRuleGetAllQry, List<ApprovalRuleDto>>, ApprovalRuleGetAllQryHndlr>();
        services.AddScoped<ICommandHandler<ProjectApprovalStepAddCmd, bool>, ProjectApprovalStepAddCmdHndlr>();
        services.AddScoped<IQueryHandler<UserGetByIdQry, UserDto>, UserGetByIdQryHndlr>();
        services.AddScoped<IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatusDto>, ApprovalStatusGetByIdQryHndlr>();
        services.AddScoped<IQueryHandler<ProjectApprovalStepGetByIdQry, ProjectApprovalStepDto>, ProjectApprovalStepGetByIdQryHndlr>();
        services.AddScoped<ICommandHandler<ProjectApprovalStepUpdateCmd, bool>, ProjectApprovalStepUpdateCmdHndlr>();
        services.AddScoped<IQueryHandler<ProjectProposalGetByIdQry, ProjectProposalDto>, ProjectProposalGetByIdQryHndlr>();
        services.AddScoped<ICommandHandler<ProjectProposalUpdateCmd, bool>, ProjectProposalUpdateCmdHndlr>();


        ///
        services.AddScoped<ICommandHandler<ProjectApprovalStepAddCmd, bool>, ProjectApprovalStepAddCmdHndlr>();
        services.AddScoped<IQueryHandler<ProjectApprovalStepGetAllQry, List<ProjectApprovalStepDto>>,ProjectApprovalStepGetAllQryHndlr>();

        services.AddScoped<IQueryHandler<ApprovalRuleGetAllQry, List<ApprovalRuleDto>>, ApprovalRuleGetAllQryHndlr>();

        services.AddScoped<IQueryHandler<ProjectProposalGetAllQry, List<ProjectProposalDto>>, ProjectProposalGetAllQryHndlr>();

        services.AddScoped<IQueryHandler<ApprovalStatusGetByIdQry, ApprovalStatusDto>, ApprovalStatusGetByIdQryHndlr>();

        services.AddScoped<IQueryHandler<AreaGetByIdQry, AreaDto>, AreaGetByIdQryHndlr>();

        services.AddScoped<IQueryHandler<UserGetByIdQry, UserDto>, UserGetByIdQryHndlr>();


        services.AddScoped<IQueryHandler<ProjectTypeGetByIdQry, ProjectTypeDto>, ProjectTypeGetByIdQryHndlr>();

        services.AddScoped<IQueryHandler<AreaGetAllQry, List<AreaDto>>, AreaGetAllQryHndlr>();

        //Commands
        services.AddScoped<ICommandHandler<ProjectProposalAddCmd, bool>, ProjectProposalAddCmdHndlr>();


        //Services
        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IProjectTypeService, ProjectTypeService>();
        services.AddScoped<IProjectProposalService, ProjectProposalService>();

        services.AddScoped<ConsoleInput>();

        services.AddScoped<ProjectProposalService>();
        services.AddScoped<ProjectApprovalStepService>();

        services.AddScoped<Menues>();
        services.AddScoped<ConsoleInput>();
    })
.Build();
//Seed inicial de los datos en la DB 
using (var scope = host.Services.CreateScope())
{
    DataSeeder.Seed(scope.ServiceProvider);
}


//Servicios a utilizar 
var projectApprovalStepService = host.Services.GetRequiredService<ProjectApprovalStepService>();
var menues = host.Services.GetRequiredService<Menues>();
var consoleInput = host.Services.GetRequiredService<ConsoleInput>();
var presentProjectService = host.Services.GetRequiredService<ProjectProposalService>();
var calculateStepsService = host.Services.GetService<ProjectApprovalStepService>();
var getAllUsersQueryHndlr = host.Services.GetRequiredService<UserGetAllQryHndlr>();
var getAllApprovalStatusQryHndlr = host.Services.GetRequiredService<ApprovalStatusGetAllQryHndlr>();
var viewPendingStepsService = host.Services.GetRequiredService<ProjectApprovalStepService>();


int opt = Menues.MainMenu();

while (opt != 0)
{
    Console.Clear(); // Limpia la consola en cada iteración del menú

    switch (opt)
    {
        case 1:
            menues.ViewProjects();
            
            Console.WriteLine("Presione cualquier tecla para volver al menu principal...");
            Console.ReadKey();
            opt = -1;
            break;

        case 2:
            // Pregunta ID
            var query = new UserGetAllQry();
            var users = getAllUsersQueryHndlr.Handle(query);

            Console.WriteLine("=====================================");
            Console.WriteLine("         Lista de Usuarios");
            Console.WriteLine("=====================================\n");

            int counter = 1;

            foreach (var user in users)
            {
                Console.WriteLine($" Usuario N° {counter}");
                Console.WriteLine($" ├─ ID:    {user.Id}");
                Console.WriteLine($" ├─ Nombre: {user.Name}");
                Console.WriteLine($" └─ Rol:   {user.Role}");
                Console.WriteLine("-------------------------------------\n");
                counter++;
            }

            var userSelectedId = ConsoleInput.ReadInt("Seleccione su ID de usuario");


            UserDto? selectedUser = users.FirstOrDefault(u => u.Id == userSelectedId);

            if (selectedUser == null)
            {
                Console.WriteLine("El usuario seleccionado no es correcto.");
                Console.WriteLine("Volviendo al menu principal, presione cualquier tecla...");
                Console.ReadLine();
                opt = -1;
                break;
            }

            // Lógica para agregar proyectos

            var project = consoleInput.SolicitarProyecto(selectedUser);
            var result = presentProjectService.present(project);



            if (result.Result == false) 
            {
                Console.WriteLine("°Error°" + result.Message);

                Console.WriteLine("0 -> Volver");
                Console.WriteLine("1 -> Intentar nuevamente");
                
                opt = ConsoleInput.ReadInt("Seleccione la opción");

                if (opt == 1) { opt = 2;break; }
            }

            if (result.Result == true) 
            {
            Console.WriteLine("Calculando pasos necesarios...");
            
            var steps = calculateStepsService.CalculateSteps(project);

            if (steps.Count > 0)
                {
                    projectApprovalStepService.CreateSteps(steps, project);
                    Console.WriteLine("Projecto creado correctamente, y han sido calculados los pasos a seguir. Presione cualquier tecla para volver al menu principal");
                }

                Console.ReadLine();
            }
            opt = -1;
            break;

        case 3:
            // Pregunta ID
            query = new UserGetAllQry();
            users = getAllUsersQueryHndlr.Handle(query);

            Console.WriteLine("=====================================");
            Console.WriteLine("         Lista de Usuarios");
            Console.WriteLine("=====================================\n");

            counter = 1;

            foreach (var user in users)
            {
                Console.WriteLine($" Usuario N° {counter}");
                Console.WriteLine($" ├─ ID:    {user.Id}");
                Console.WriteLine($" ├─ Nombre: {user.Name}");
                Console.WriteLine($" └─ Rol:   {user.Role}");
                Console.WriteLine("-------------------------------------\n");
                counter++;
            }

            userSelectedId = ConsoleInput.ReadInt("Seleccione su ID de usuario");

            
            selectedUser = users.FirstOrDefault(u => u.Id == userSelectedId);

            if (selectedUser == null) 
            {
                Console.WriteLine("El usuario seleccionado no es correcto.");
                Console.WriteLine("Volviendo al menu principal, presione cualquier tecla...");
                Console.ReadLine();
                opt = -1; 
                break; 
            }

            var pendingSteps = viewPendingStepsService.ViewPendingWithRole(new UserDto()
            {
                Id = selectedUser.Id,
                Role = selectedUser.Role,
            });

            Console.Clear();

            Console.WriteLine("El usuario seleccionado tiene las siguientes aprobaciones pendientes: ");
            foreach (var pendingStep in pendingSteps)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine($" Step ID:           {pendingStep.Id}");
                Console.WriteLine($" Step Order:        {pendingStep.StepOrder}");
                Console.WriteLine($" Project ID:        {pendingStep.ProjectProposalId}");
                Console.WriteLine($" Approver Role ID:  {pendingStep.ApproverRoleId}");
                Console.WriteLine($" Status:            {pendingStep.Status}");
                Console.WriteLine("===========================================\n");
            }

            
            //MOSTRAR TODOS LOS STEPS QUE PUEDE APROBAR/RECHAZAR


            ProjectApprovalStepDto? selectedPendingStep = null;

            while (selectedPendingStep == null)
            {
                var selectedStepId = ConsoleInput.ReadInt("Seleccione el ID del paso que desea aprobar o rechazar (0 para volver al menú)");

                if (selectedStepId == 0)
                {
                    // Sale al menú principal
                    opt = -1;
                    break;
                }

                selectedPendingStep = pendingSteps.FirstOrDefault(step => step.Id == selectedStepId);

                if (selectedPendingStep == null)
                {
                    Console.WriteLine("ID no válido. Intente nuevamente o ingrese 0 para volver al menú.");
                }
            }

            
            if (selectedPendingStep != null)
            {
                var statusQuery = new ApprovalStatusGetAllQry();
                var statuses = getAllApprovalStatusQryHndlr.Handle(statusQuery);

                Console.WriteLine("Estados disponibles para selección:");
                Console.WriteLine("==================================");

                foreach (var status in statuses)
                {
                    Console.WriteLine($"ID: {status.Id} - Nombre: {status.Name}");
                }   

                Console.WriteLine("==================================");

                int selectedStatusId;
                var statusValido = false;

                do
                {
                    selectedStatusId = ConsoleInput.ReadInt("Ingrese el ID del nuevo estado (0 para cancelar):");

                    if (selectedStatusId == 0)
                    {
                        Console.WriteLine("Operación cancelada. Volviendo al menú principal...");
                        opt = -1; break ; 
                    }

                    statusValido = statuses.Any(s => s.Id == selectedStatusId);

                    if (!statusValido)
                    {
                        Console.WriteLine("ID inválido. Por favor intente nuevamente.");
                    }

                } while (!statusValido);

                // En este punto, selectedStatusId contiene un valor válido.
                var selectedStatus = statuses.First(s => s.Id == selectedStatusId);


                var changeStatusResult = projectApprovalStepService.UpdateStatus(new updateApprovalStatusDto()
                {
                    step = selectedPendingStep.Id,
                    newStatus = selectedStatus.Id,
                    user = selectedUser.Id
                });

                if (changeStatusResult) 
                {
                    Console.WriteLine("Paso cambiado correctamente, volviendo al menu principal");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                }
                Console.ReadKey(true);
                opt = -1;
            }


            break;
        case 4:
            var pendings = viewPendingStepsService.ViewPendingAll();

            int cont = 1;

            foreach (var step in pendings)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine($"       Proyecto N° {cont}");
                Console.WriteLine("===========================================");
                Console.WriteLine($"ID: {step.Id}");
                Console.WriteLine($"Project Proposal ID: {step.ProjectProposalId}");
                Console.WriteLine($"Step Order: {step.StepOrder}");
                Console.WriteLine($"Status: {step.Status}");
                Console.WriteLine($"Approver Role ID: {step.ApproverRoleId}");
                Console.WriteLine($"Decision Date: {step.DecisionDate?.ToString("yyyy-MM-dd") ?? "No decidida"}");
                Console.WriteLine($"Observations: {step.Observations ?? "Sin observaciones"}");
                Console.WriteLine("\n");

                cont++;
            }

            Console.WriteLine("Presione cualquier tecla para volver");
            Console.ReadKey();
            opt = -1;
            break;
        case -1:
            // Volver a cargar el menu principal
            opt = Menues.MainMenu();
            break;
        
        default:
            Console.WriteLine("Opción no válida.");
            opt = Menues.MainMenu();
            break;
    }
}

Console.WriteLine("Gracias por usar el sistema. ¡Hasta luego!");

Console.ReadKey();


