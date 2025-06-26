using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectTypeRepository : IProjectTypeRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public ProjectTypeRepository(ProjectApprovalDbContext context) { _context = context; }

        public async Task Add(ProjectType type)
        {
            var efType = new Infrastructure.Persistence.Entities.ProjectType()
            {
                Name = type.Name
            };

            await _context.ProjectTypes.AddAsync(efType);
            await _context.SaveChangesAsync();          
        }

        public async Task<bool> Delete(int id)
        {
            var efType = await _context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == id);

            if (efType == null) return false;

            _context.ProjectTypes.Remove(efType);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<ProjectType>> GetAll()
        {
            var efTypes = await _context.ProjectTypes.ToListAsync(); // lista de entidades EF

            var domainTypes = new List<Domain.Entities.ProjectType>();


            foreach (var efType in efTypes)
            {
                var type = new Domain.Entities.ProjectType()
                {
                    Id = efType.Id,
                    Name = efType.Name

                };
                domainTypes.Add(type);
            }

            return domainTypes;
        }

        public async Task<ProjectType?> GetById(int id)
        {
            var efType = await _context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == id);
            return efType == null ? null : new ProjectType() { Id = efType.Id, Name = efType.Name }; // Mapea a dominio
        }

        public async Task<bool> Update(ProjectType type)
        {
            var id = type.Id;

            var efType = await _context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == id);
            if (efType == null) return false;

            efType.Name = type.Name;
            _context.SaveChanges();
            return true;

        }
    }
}
