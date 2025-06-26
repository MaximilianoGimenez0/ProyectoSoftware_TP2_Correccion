using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApproverRole.Queries.GetById;
using Application.Dtos;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IQueryHandler<UserGetByIdQry, User?> _userGetByIdHandler;
        private readonly IQueryHandler<UserGetAllQry, List<UserDto>> _userGetAllHandler;
        private readonly IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?> _approverRoleGetByIdHandler;
        public UserService(IQueryHandler<UserGetByIdQry, User?> userGetByIdHandler, IQueryHandler<UserGetAllQry, List<UserDto>> userGetAllHandler, IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?> approverRoleByIdHandler)
        {
            _userGetByIdHandler = userGetByIdHandler;
            _userGetAllHandler = userGetAllHandler;
            _approverRoleGetByIdHandler = approverRoleByIdHandler;
        }

        public async Task<List<Dtos.Responses.UsersResponse>> GetAll()
        {
            var response = new List<Dtos.Responses.UsersResponse>();

            var users = await _userGetAllHandler.Handle(new UserGetAllQry());
            foreach (var user in users) 
            {
                var tempUserRole = await _approverRoleGetByIdHandler.Handle(new ApproverRoleGetByIdQry(new ApproverRoleDto() { Id = user.Role }));
                var tempRole = new GenericResponse() { id = tempUserRole.Id,name = tempUserRole.Name };

                var tempUser = new Dtos.Responses.UsersResponse() 
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    role = tempRole
                };
                response.Add(tempUser);
            }

            return response;
        }

        public async Task<User?> GetById(int id)
        {
            var userDto = await _userGetByIdHandler.Handle(new UserGetByIdQry(new UserDto() { Id = id}));
            if (userDto == null) { return null; }
            return userDto;
        }
    }
}
