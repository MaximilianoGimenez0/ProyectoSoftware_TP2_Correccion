using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    public ApproverRole UserRole { get; set; }

    public IList<ProjectProposal> ProjectProposals { get; set; }

    public IList<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
}
