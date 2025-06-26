using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApproverRole.Commands.Update
{
    public class ApproverRoleUpdateCmd
    {
        public Domain.Entities.ApproverRole ApproverRoleDto { get; set; }

        public ApproverRoleUpdateCmd(Domain.Entities.ApproverRole approverRoleDto) { ApproverRoleDto = approverRoleDto; }
    }
}
