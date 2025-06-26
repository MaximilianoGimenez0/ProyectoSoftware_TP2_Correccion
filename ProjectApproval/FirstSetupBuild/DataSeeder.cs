using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApprovalRules.Comands.Add;
using Application.ApprovalStatuses.Commands.Add;
using Application.ApproverRole.Commands.Add;
using Application.Areas.Commands.Add;
using Application.Dtos;
using Application.ProjectTypes.Commands.Add;
using Application.Users.Commands.Add;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProjectApproval.FirstSetupBuild
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider services)
        {
            var db = services.GetRequiredService<ProjectApprovalDbContext>();

            if (!db.Users.Any() && !db.Areas.Any())
            {

                #region FIRST SETUP
                //FRIST SETUP

                //ROLES
                var approverRole1 = new ApproverRoleDto() { Name = "Líder de Área", };
                var approverRole2 = new ApproverRoleDto() { Name = "Gerente", };
                var approverRole3 = new ApproverRoleDto() { Name = "Director", };
                var approverRole4 = new ApproverRoleDto() { Name = "Comité Técnico", };

                var roleCommand = new ApproverRoleAddCmd(approverRole1);
                var roleHandler = services.GetRequiredService<ApproverRoleAddCmdHndlr>();
                roleHandler.Handle(roleCommand);

                roleCommand = new ApproverRoleAddCmd(approverRole2);
                roleHandler.Handle(roleCommand);

                roleCommand = new ApproverRoleAddCmd(approverRole3);
                roleHandler.Handle(roleCommand);

                roleCommand = new ApproverRoleAddCmd(approverRole4);
                roleHandler.Handle(roleCommand);

                //AREAS

                var area1 = new AreaDto() { Name = "Finanzas" };
                var area2 = new AreaDto() { Name = "Tecnología" };
                var area3 = new AreaDto() { Name = "Recursos Humanos" };
                var area4 = new AreaDto() { Name = "Operaciones" };

                var areaCommand = new AreaAddCmd(area1);
                var areaHandler = services.GetRequiredService<AreaAddCmdHndlr>();
                areaHandler.Handle(areaCommand);

                areaCommand = new AreaAddCmd(area2);
                areaHandler.Handle(areaCommand);

                areaCommand = new AreaAddCmd(area3);
                areaHandler.Handle(areaCommand);

                areaCommand = new AreaAddCmd(area4);
                areaHandler.Handle(areaCommand);


                //APPROVALSTATUS
                var approvalStatus1 = new ApprovalStatusDto() { Name = "Pending", };
                var approvalStatus2 = new ApprovalStatusDto() { Name = "Approved", };
                var approvalStatus3 = new ApprovalStatusDto() { Name = "Rejected", };
                var approvalStatus4 = new ApprovalStatusDto() { Name = "Observed", };

                var approvalStatusCommand = new ApprovalStatusAddCmd(approvalStatus1);
                var approvalStatusHandler = services.GetRequiredService<ApprovalStatusAddCmdHndlr>();
                approvalStatusHandler.Handle(approvalStatusCommand);

                approvalStatusCommand = new ApprovalStatusAddCmd(approvalStatus2);
                approvalStatusHandler.Handle(approvalStatusCommand);

                approvalStatusCommand = new ApprovalStatusAddCmd(approvalStatus3);
                approvalStatusHandler.Handle(approvalStatusCommand);

                approvalStatusCommand = new ApprovalStatusAddCmd(approvalStatus4);
                approvalStatusHandler.Handle(approvalStatusCommand);

                //PROJECT TYPES

                var projectType1 = new ProjectTypeDto() { Name = "Mejora de Procesos" };
                var projectType2 = new ProjectTypeDto() { Name = "Innovación y Desarrollo" };
                var projectType3 = new ProjectTypeDto() { Name = "Infraestructura" };
                var projectType4 = new ProjectTypeDto() { Name = "Capacitación Interna" };

                var projectTypeCommand = new ProjectTypeAddCmd(projectType1);
                var projectTypeHandler = services.GetRequiredService<ProjectTypeAddCmdHndlr>();
                projectTypeHandler.Handle(projectTypeCommand);

                projectTypeCommand = new ProjectTypeAddCmd(projectType2);
                projectTypeHandler.Handle(projectTypeCommand);

                projectTypeCommand = new ProjectTypeAddCmd(projectType3);
                projectTypeHandler.Handle(projectTypeCommand);

                projectTypeCommand = new ProjectTypeAddCmd(projectType4);
                projectTypeHandler.Handle(projectTypeCommand);


                //USERS

                var User1 = new UserDto() { Name = "José Ferreyra", Email = "jferreyra@unaj.com", Role = 2 };
                var User2 = new UserDto() { Name = "Ana Lucero", Email = "alucero@unaj.com", Role = 1 };
                var User3 = new UserDto() { Name = "Gonzalo Molinas", Email = "gmolinas@unaj.com", Role = 2 };
                var User4 = new UserDto() { Name = "Lucas Olivera", Email = "lolivera@unaj.com", Role = 3 };
                var User5 = new UserDto() { Name = "Danilo Fagundez", Email = "dfagundez@unaj.com", Role = 4 };
                var User6 = new UserDto() { Name = "Gabriel Galli", Email = "ggalli@unaj.com", Role = 4 };

                var userCommand = new UserAddCmd(User1);
                var userHandler = services.GetRequiredService<UserAddCmdHndlr>();
                userHandler.Handle(userCommand);

                userCommand = new UserAddCmd(User2);
                userHandler.Handle(userCommand);

                userCommand = new UserAddCmd(User3);
                userHandler.Handle(userCommand);

                userCommand = new UserAddCmd(User4);
                userHandler.Handle(userCommand);

                userCommand = new UserAddCmd(User5);
                userHandler.Handle(userCommand);

                userCommand = new UserAddCmd(User6);
                userHandler.Handle(userCommand);

                //RULES

                var rule1 = new ApprovalRuleDto() { MinAmount = 0, MaxAmount = 100000, Area = null, Type = null, StepOrder = 1, ApproverRoleId = 1 };
                var rule2 = new ApprovalRuleDto() { MinAmount = 5000, MaxAmount = 20000, Area = null, Type = null, StepOrder = 2, ApproverRoleId = 2 };
                var rule3 = new ApprovalRuleDto() { MinAmount = 0, MaxAmount = 20000, Area = 2, Type = 2, StepOrder = 1, ApproverRoleId = 2 };
                var rule4 = new ApprovalRuleDto() { MinAmount = 20000, MaxAmount = 0, Area = null, Type = null, StepOrder = 3, ApproverRoleId = 3 };
                var rule5 = new ApprovalRuleDto() { MinAmount = 5000, MaxAmount = 0, Area = 1, Type = 1, StepOrder = 2, ApproverRoleId = 2 };
                var rule6 = new ApprovalRuleDto() { MinAmount = 0, MaxAmount = 10000, Area = null, Type = 2, StepOrder = 1, ApproverRoleId = 1 };
                var rule7 = new ApprovalRuleDto() { MinAmount = 0, MaxAmount = 10000, Area = 2, Type = 1, StepOrder = 1, ApproverRoleId = 4 };
                var rule8 = new ApprovalRuleDto() { MinAmount = 10000, MaxAmount = 30000, Area = 3, Type = null, StepOrder = 2, ApproverRoleId = 2 };
                var rule9 = new ApprovalRuleDto() { MinAmount = 30000, MaxAmount = 0, Area = 2, Type = null, StepOrder = 2, ApproverRoleId = 3 };
                var rule10 = new ApprovalRuleDto() { MinAmount = 0, MaxAmount = 50000, Area = null, Type = 4, StepOrder = 1, ApproverRoleId = 4 };

                var ruleCommand = new ApprovalRuleAddCmd(rule1);
                var ruleHandler = services.GetRequiredService<ApprovalRuleAddCmdHndlr>();
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule2);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule3);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule4);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule5);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule6);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule7);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule8);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule9);
                ruleHandler.Handle(ruleCommand);

                ruleCommand = new ApprovalRuleAddCmd(rule10);
                ruleHandler.Handle(ruleCommand);



                #endregion
            }
        }
    }

}
