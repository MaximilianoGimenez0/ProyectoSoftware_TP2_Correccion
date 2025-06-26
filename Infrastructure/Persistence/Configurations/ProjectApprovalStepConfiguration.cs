using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class ProjectApprovalStepConfiguration : IEntityTypeConfiguration<ProjectApprovalStep>
    {
        public void Configure(EntityTypeBuilder<ProjectApprovalStep> builder)
        {
            builder.ToTable("ProjectApprovalStep");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.StepOrder)
                .IsRequired();

            builder.Property(p => p.DecisionDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(p => p.Observations)
                .IsRequired(false)
                .HasColumnType("varchar(max)");

            builder.HasOne(p => p.StepProjectProposal)
                .WithMany(c => c.ProjectApprovalSteps)
                .HasForeignKey(p => p.ProjectProposalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.StepUser)
                .WithMany(c => c.ProjectApprovalSteps)
                .HasForeignKey(p => p.ApproverUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.StepApproverRole)
                .WithMany(c => c.ProjectApprovalSteps)
                .HasForeignKey(p => p.ApproverRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.StepApprovalStatus)
                .WithMany(c => c.ProjectApprovalSteps)
                .HasForeignKey(p => p.Status)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
