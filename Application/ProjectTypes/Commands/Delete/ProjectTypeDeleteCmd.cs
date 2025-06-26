using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectTypes.Commands.Delete
{
    public class ProjectTypeDeleteCmd
    {
        public ProjectTypeDto PrjectTypeDto { get; set; }

        public ProjectTypeDeleteCmd(ProjectTypeDto projectTypeDto) 
        {
            PrjectTypeDto = projectTypeDto; 
        }
    }
}
