using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ApprovalStatusRepository : IApprovalStatusRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public ApprovalStatusRepository(ProjectApprovalDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.ApprovalStatus status)
        {
            var efStatus = new Infrastructure.Persistence.Entities.ApprovalStatus()
            {
                Name = status.Name
            };

            await _context.Status.AddAsync(efStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var efStatus = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (efStatus == null) return false;
            
            _context.Status.Remove(efStatus);
            await _context.SaveChangesAsync();
            return true;
                        
        }

        public async Task<List<Domain.Entities.ApprovalStatus>> GetAll()
        {
            var efStatuses = await _context.Status.ToListAsync(); // lista de entidades EF

            var domainStatuses = new List<Domain.Entities.ApprovalStatus>();


            foreach (var efStatus in efStatuses)
            {
                var status = new Domain.Entities.ApprovalStatus(efStatus.Name)
                {
                    Id = efStatus.Id
                };
                domainStatuses.Add(status);
            }

            return domainStatuses;
        }

        public async Task<Domain.Entities.ApprovalStatus?> GetById(int id)
        {
            var efStatus = await _context.Status.FirstOrDefaultAsync(r => r.Id == id);
            return efStatus == null ? null : new Domain.Entities.ApprovalStatus(efStatus.Name) { Id = efStatus.Id }; // Mapea a dominio
        }

        public async Task<bool> Update(Domain.Entities.ApprovalStatus status)
        {
            var id = status.Id;

            var efStatus = await _context.Status.FirstOrDefaultAsync(r => r.Id == id);
            if (efStatus == null) return false;

            efStatus.Name = status.Name;
            _context.SaveChanges();
            return true;

        }
    }
}
