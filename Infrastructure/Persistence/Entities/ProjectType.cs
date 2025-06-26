using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Entities
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ApprovalRule> ApprovalRules { get; set; }

        public IList<ProjectProposal> ProjectProposals { get; set; }
    }
}
