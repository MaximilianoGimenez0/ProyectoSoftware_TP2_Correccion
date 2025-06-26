using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.ApproverRole.Commands.Add
{
    public class ApproverRoleAddCmd
    {
        public Domain.Entities.ApproverRole ApproverRoleDto { get; set; }

        public ApproverRoleAddCmd(Domain.Entities.ApproverRole approverRoleDto) 
        {
            this.ApproverRoleDto = approverRoleDto;
        }
    }
}
