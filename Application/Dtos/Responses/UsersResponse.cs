using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Responses
{
    public class UsersResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? email { get; set; }
        public GenericResponse role { get; set; }
    }
}
