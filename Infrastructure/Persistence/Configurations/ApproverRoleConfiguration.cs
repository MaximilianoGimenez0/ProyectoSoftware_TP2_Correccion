using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Entities;
namespace Infrastructure.Persistence
{
    public class ApproverRoleConfiguration : IEntityTypeConfiguration<ApproverRole>
    {
        public void Configure(EntityTypeBuilder<ApproverRole> builder)
        {
            builder.ToTable("ApproverRole");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()  
                .HasMaxLength(25)
                .HasColumnType("varchar(25)");
        }
    }
}