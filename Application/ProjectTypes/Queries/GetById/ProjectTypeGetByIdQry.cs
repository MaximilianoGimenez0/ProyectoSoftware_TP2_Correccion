using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.ProjectTypes.Queries.GetById
{
    public class ProjectTypeGetByIdQry
    {
        public ProjectTypeDto ProjectTypeDto { get; set; }

        public ProjectTypeGetByIdQry(ProjectTypeDto projectTypeDto)
        {
            ProjectTypeDto = projectTypeDto;
        }
    }
}
