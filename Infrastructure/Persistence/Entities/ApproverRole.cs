using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Entities
{
    public class ApproverRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }
        public IList<ApprovalRule> ApprovalRules { get; set; }

        public IList<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
    }
}
