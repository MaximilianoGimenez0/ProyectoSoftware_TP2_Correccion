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
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired() 
            .HasMaxLength(25) 
            .HasColumnType("varchar(25)");

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(25)
            .HasColumnType("varchar(25)");
        

        builder.HasOne(p => p.UserRole)
            .WithMany(c => c.Users)
            .HasForeignKey(p => p.Role)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
}