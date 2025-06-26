using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ApproverRole.Queries.GetById
{
    public class ApproverRoleGetByIdQry
    {
        public ApproverRoleDto ApproverRoleDto { get; set; }

        public ApproverRoleGetByIdQry(ApproverRoleDto approverRoleDto) { ApproverRoleDto = approverRoleDto; }
    }
}
