using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ApproverRoleRepository  : IApproverRoleRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public ApproverRoleRepository(ProjectApprovalDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.ApproverRole role)
        {
            var efRole = new Infrastructure.Persistence.Entities.ApproverRole()
            {
                Name = role.Name
            };

            await _context.Roles.AddAsync(efRole);
            await _context.SaveChangesAsync();
            
        }

        public async Task<bool> Delete(int id)
        {
            var efRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (efRole == null) return false;
            
             _context.Roles.Remove(efRole);
             await _context.SaveChangesAsync();
             return true; 
           
        }

        public async Task<List<Domain.Entities.ApproverRole>> GetAll()
        {
            var efRoles = await _context.Roles.ToListAsync(); // lista de entidades EF

            var domainRoles = new List<Domain.Entities.ApproverRole>();


            foreach (var efRole in efRoles)
            {
                var role = new Domain.Entities.ApproverRole()
                {
                    Id = efRole.Id,
                    Name = efRole.Name
                };
                domainRoles.Add(role);
            }

            return domainRoles;
        }

        public async Task<Domain.Entities.ApproverRole?> GetById(int id)
        {
            var efRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return efRole == null ? null : new Domain.Entities.ApproverRole() { Id = efRole.Id, Name = efRole.Name}; // Mapea a dominio
        }

        public async Task<bool> Update(Domain.Entities.ApproverRole role)
        {
            var id = role.Id;

            var efRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if(efRole == null) return false;
          
            efRole.Name = role.Name;
            _context.SaveChanges();
            return true;
        
        }
    }
}
