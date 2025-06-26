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
    public class ProjectProposalConfiguration : IEntityTypeConfiguration<ProjectProposal>
    {
      
        public void Configure(EntityTypeBuilder<ProjectProposal> builder)
        {
            builder.ToTable("ProjectProposal");

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Description)
                .IsRequired()
                 .HasColumnType("varchar(max)");

            builder.Property(p => p.EstimatedAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); //Aclaro la precisión del decimal

            builder.Property(p => p.EstimatedDuration)
                .IsRequired();

            builder.Property(p => p.CreateAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasOne(p => p.ProjectProposalArea)
            .WithMany(c => c.ProjectProposals)
            .HasForeignKey(p => p.Area)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProjectProposalType)
                .WithMany(c => c.ProjectProposals)
                .HasForeignKey(p => p.Type)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProjectProposalApprovalStatus)
                .WithMany(c => c.ProjectProposals)
                .HasForeignKey(p => p.Status)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProjectProposalUser)
                .WithMany(c => c.ProjectProposals)
                .HasForeignKey(p => p.CreateBy)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}