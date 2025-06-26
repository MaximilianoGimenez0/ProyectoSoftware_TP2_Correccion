using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApproverRole.Queries.GetALL;
using Application.ApproverRole.Queries.GetById;
using Application.Areas.Queries.GetAll;
using Application.Dtos;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Users.Queries.GetById;
using Domain.Entities;

namespace Application.Services
{
    public class ApproverRoleService : IApproverRoleService
    {
        private readonly IQueryHandler<ApproverRoleGetByIdQry,Domain.Entities.ApproverRole?> _approverRoleGetByIdHandler;
        private readonly IQueryHandler<ApproverRoleGetAllQry, List<ApproverRoleDto>> _approverRoleGetAllHandler;

        public ApproverRoleService(IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?> approverRoleGetByIdHandler, IQueryHandler<ApproverRoleGetAllQry, List<ApproverRoleDto>> approverRoleGetAllHandler)
        {
            _approverRoleGetByIdHandler = approverRoleGetByIdHandler;
            _approverRoleGetAllHandler = approverRoleGetAllHandler;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var results = new List<GenericResponse>();
            var roles = await _approverRoleGetAllHandler.Handle(new ApproverRoleGetAllQry());
            foreach (var role in roles)
            {
                var tempRole = new GenericResponse() { id = role.Id, name = role.Name };
                results.Add(tempRole);
            }

            return results;
        }

        public async Task<Domain.Entities.ApproverRole?> GetById(int id)
        {
            var roleDto = await _approverRoleGetByIdHandler.Handle(new ApproverRoleGetByIdQry(new ApproverRoleDto() { Id = id }));
            return roleDto;
        }
    }
}
