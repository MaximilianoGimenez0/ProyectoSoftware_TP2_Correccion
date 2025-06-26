using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApproverRole.Queries.GetALL;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApproverRole.Queries.GetApproverRoles
{
     public class ApproverRoleGetAllQryHndlr : IQueryHandler<ApproverRoleGetAllQry,List<ApproverRoleDto>>
    {
        private readonly IApproverRoleRepository _roleRepository;

        public ApproverRoleGetAllQryHndlr (IApproverRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<ApproverRoleDto>> Handle(ApproverRoleGetAllQry query)
        {
            var roles = await _roleRepository.GetAll(); // lista de entidades EF

            var dtoRoles = new List<Application.Dtos.ApproverRoleDto>();


            foreach (var role in roles)
            {
                var temp = new Application.Dtos.ApproverRoleDto()
                {
                    Name = role.Name,
                    Id = role.Id,
                };
                dtoRoles.Add(temp);
            }

            return dtoRoles;
        }
    }
}
