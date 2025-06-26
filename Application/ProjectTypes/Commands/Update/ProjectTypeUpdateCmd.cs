using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectTypes.Commands.Update
{
    public class ProjectTypeUpdateCmd
    {
        public Domain.Entities.ProjectType ProjectTypeDto { get; set; }

        public ProjectTypeUpdateCmd(Domain.Entities.ProjectType projectTypeDto) 
        {
            ProjectTypeDto = projectTypeDto;
        }
    }
}
