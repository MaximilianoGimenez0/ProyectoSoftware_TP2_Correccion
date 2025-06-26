using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectTypes.Commands.Add
{
    public class ProjectTypeAddCmd
    {
        public Domain.Entities.ProjectType ProjectTypeDto { get; set; }

        public ProjectTypeAddCmd(Domain.Entities.ProjectType projectTypeDto)
        {
            ProjectTypeDto = projectTypeDto;
        }
    }
}
