using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AreaRepository : IAreaRepository
    {

        private readonly ProjectApprovalDbContext _context;

        public AreaRepository(ProjectApprovalDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.Area area)
        {
            var efArea = new Infrastructure.Persistence.Entities.Area()
            {
                Name = area.Name
            };

            await _context.Areas.AddAsync(efArea);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var efArea = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);
            if (efArea == null) return false;

            _context.Areas.Remove(efArea);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Domain.Entities.Area>> GetAll()
        {
            var efAreas = await _context.Areas.ToListAsync(); // lista de entidades EF

            var domainAreas = new List<Domain.Entities.Area>();


            foreach (var efArea in efAreas)
            {
                var area = new Domain.Entities.Area()
                {
                    Name = efArea.Name,
                    Id = efArea.Id
                };
                domainAreas.Add(area);
            }

            return domainAreas;
        }

        public async Task<Domain.Entities.Area?> GetById(int id)
        {
            var efArea = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);
            return efArea == null ? null : new Domain.Entities.Area() { Id = efArea.Id, Name = efArea.Name }; // Mapea a dominio
        }

        public async Task<bool> Update(Domain.Entities.Area area)
        {
            var id = area.Id;

            var efArea = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);
            if (efArea == null) return false;

            efArea.Name = area.Name;
            _context.SaveChanges();
            return true;

        }
    }
}
