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
    public class UserRepository : IUserRepository
    {
        private readonly ProjectApprovalDbContext _context;

        public UserRepository(ProjectApprovalDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.User user)
        {
            var efUser = new Infrastructure.Persistence.Entities.User()
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };

          await _context.Users.AddAsync(efUser);
          await _context.SaveChangesAsync();
          
        }

        public async Task<bool> Delete(int id)
        {
            var efuser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (efuser != null)
            {
                _context.Users.Remove(efuser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Domain.Entities.User>> GetAll()
        {
            var efUsers = await _context.Users
            .Include(u => u.UserRole)
            .ToListAsync();

            var domainUsers = new List<Domain.Entities.User>();

            foreach (var efUser in efUsers)
            {
                var user = new Domain.Entities.User
                {
                    Id = efUser.Id,
                    Name = efUser.Name,
                    Email = efUser.Email,
                    Role = efUser.Role,
                    UserRole = new Domain.Entities.ApproverRole { Id = efUser.UserRole.Id, Name = efUser.UserRole.Name }
                };                    

                domainUsers.Add(user);

            }
            return domainUsers;
        }

        public async Task<Domain.Entities.User?> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user == null ? null : new Domain.Entities.User 
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                UserRole = new Domain.Entities.ApproverRole 
                    {
                        Id = user.UserRole.Id,Name = user.UserRole.Name
                    }
            };
        }

        public async Task<bool> Update(Domain.Entities.User user)
        {
            var id = user.Id;
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userDb == null) return false;

            userDb.Name = user.Name;
            userDb.Email = user.Email;
            userDb.Role = user.Role;
            
            await _context.SaveChangesAsync();
            
            return true;
            
        }
    }
}
