using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class updateApprovalStatusDto
    {
        public int? user {  get; set; }
        public long? step { get; set; }
        public int? newStatus{ get; set; }
        public string? observation {  get; set; }
    }
}
