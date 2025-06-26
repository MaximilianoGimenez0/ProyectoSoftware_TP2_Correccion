using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class ApprovalStatusConfiguration : IEntityTypeConfiguration<Entities.ApprovalStatus>
    {
        public void Configure(EntityTypeBuilder<Entities.ApprovalStatus> builder)
        {
            builder.ToTable("ApprovalStatus");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()  
                .HasMaxLength(25)
                .HasColumnType("varchar(25)");  
        }
    }
}