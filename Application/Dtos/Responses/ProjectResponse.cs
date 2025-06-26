using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Responses
{
    public class ProjectResponse
    {
        public Guid? id { get; set; }
        public string? title {  get; set; }
        public string? description { get; set; }
        public decimal? amount { get; set; }
        public int? duration { get; set; }
        public UsersResponse? user { get; set; }
        public GenericResponse? area { get; set; }
        public GenericResponse? status { get; set; }
        public GenericResponse? type { get; set; }
        public List<ApprovalStepResponse>? steps { get; set; }

        
    }

}
