using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApproverRole.Commands.Delete
{
    public class ApproverRoleDeleteCmd
    {
        public ApproverRoleDto ApproverRoleDto { get; set; }

        public ApproverRoleDeleteCmd(ApproverRoleDto approverRoleDto) { ApproverRoleDto = approverRoleDto; }
    }
}
