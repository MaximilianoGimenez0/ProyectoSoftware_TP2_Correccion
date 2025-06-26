using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApproverRole.Queries.GetALL;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApproverRole.Queries.GetById
{
    public class ApproverRoleGetByIdQryHndlr : IQueryHandler<ApproverRoleGetByIdQry, Domain.Entities.ApproverRole?>
    {
        private readonly IApproverRoleRepository _roleRepository;
        public ApproverRoleGetByIdQryHndlr(IApproverRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Domain.Entities.ApproverRole?> Handle(ApproverRoleGetByIdQry query)
        {
            if (query.ApproverRoleDto.Id == null) { return null; }
            var id = query.ApproverRoleDto.Id.Value;
            var role = await _roleRepository.GetById(id);
            
            return role;
        }
    }
}
