using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProjectApprovalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ApprovalRule> Rules { get; set; }

        public DbSet<ApproverRole> Roles { get; set; }

        public DbSet<ApprovalStatus> Status { get; set; }

        public DbSet<ProjectType> ProjectTypes { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<ProjectProposal> ProjectProposals { get; set;}

        public DbSet<ProjectApprovalStep> ProjectApprovalSteps { get; set; }

        public ProjectApprovalDbContext (DbContextOptions<ProjectApprovalDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Registra las configuraciones
            modelBuilder.ApplyConfiguration(new ApproverRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovalStatusConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovalRuleConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectApprovalStepConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectProposalConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);

            // ROLES
            modelBuilder.Entity<ApproverRole>().HasData(
                new ApproverRole { Id = 1, Name = "Líder de Área" },
                new ApproverRole { Id = 2, Name = "Gerente" },
                new ApproverRole { Id = 3, Name = "Director" },
                new ApproverRole { Id = 4, Name = "Comité Técnico" }
            );

            // ÁREAS
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Finanzas" },
                new Area { Id = 2, Name = "Tecnología" },
                new Area { Id = 3, Name = "Recursos Humanos" },
                new Area { Id = 4, Name = "Operaciones" }
            );

            // ESTADOS DE APROBACIÓN
            modelBuilder.Entity<ApprovalStatus>().HasData(
                new ApprovalStatus { Id = 1, Name = "Pending" },
                new ApprovalStatus { Id = 2, Name = "Approved" },
                new ApprovalStatus { Id = 3, Name = "Rejected" },
                new ApprovalStatus { Id = 4, Name = "Observed" }
            );

            // TIPOS DE PROYECTO
            modelBuilder.Entity<ProjectType>().HasData(
                new ProjectType { Id = 1, Name = "Mejora de Procesos" },
                new ProjectType { Id = 2, Name = "Innovación y Desarrollo" },
                new ProjectType { Id = 3, Name = "Infraestructura" },
                new ProjectType { Id = 4, Name = "Capacitación Interna" }
            );

            // USUARIOS (¡cuidado con contraseñas si las hubiera!)
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "José Ferreyra", Email = "jferreyra@unaj.com", Role = 2 },
                new User { Id = 2, Name = "Ana Lucero", Email = "alucero@unaj.com", Role = 1 },
                new User { Id = 3, Name = "Gonzalo Molinas", Email = "gmolinas@unaj.com", Role = 2 },
                new User { Id = 4, Name = "Lucas Olivera", Email = "lolivera@unaj.com", Role = 3 },
                new User { Id = 5, Name = "Danilo Fagundez", Email = "dfagundez@unaj.com", Role = 4 },
                new User { Id = 6, Name = "Gabriel Galli", Email = "ggalli@unaj.com", Role = 4 }
            );

            // REGLAS
            modelBuilder.Entity<ApprovalRule>().HasData(
                new ApprovalRule { Id = 1, MinAmount = 0, MaxAmount = 100000, StepOrder = 1, ApproverRoleId = 1 },
                new ApprovalRule { Id = 2, MinAmount = 5000, MaxAmount = 20000, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 3, MinAmount = 0, MaxAmount = 20000, StepOrder = 1, ApproverRoleId = 2, Area = 2, Type = 2 },
                new ApprovalRule { Id = 4, MinAmount = 20000, MaxAmount = 0, StepOrder = 3, ApproverRoleId = 3 },
                new ApprovalRule { Id = 5, MinAmount = 5000, MaxAmount = 0, StepOrder = 2, ApproverRoleId = 2, Area = 1, Type = 1 },
                new ApprovalRule { Id = 6, MinAmount = 0, MaxAmount = 10000, StepOrder = 1, ApproverRoleId = 1, Type = 2 },
                new ApprovalRule { Id = 7, MinAmount = 0, MaxAmount = 10000, StepOrder = 1, ApproverRoleId = 4, Area = 2, Type = 1 },
                new ApprovalRule { Id = 8, MinAmount = 10000, MaxAmount = 30000, StepOrder = 2, ApproverRoleId = 2, Area = 2 },
                new ApprovalRule { Id = 9, MinAmount = 30000, MaxAmount = 0, StepOrder = 2, ApproverRoleId = 3, Area = 3 },
                new ApprovalRule { Id = 10, MinAmount = 0, MaxAmount = 50000, StepOrder = 1, ApproverRoleId = 4, Type = 4 }
            );
        }


    }
}
