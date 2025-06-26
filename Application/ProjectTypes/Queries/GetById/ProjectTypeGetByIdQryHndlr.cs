using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.Queries.GetAll;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ProjectTypes.Queries.GetById
{
    public class ProjectTypeGetByIdQryHndlr : IQueryHandler<ProjectTypeGetByIdQry,ProjectType?>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        public ProjectTypeGetByIdQryHndlr(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        public async Task<ProjectType?> Handle(ProjectTypeGetByIdQry query)
        {
            if (query.ProjectTypeDto.Id == null) { return null; }

            var id = query.ProjectTypeDto.Id.Value;
            var type = await _projectTypeRepository.GetById(id);

            if (type == null) { return null; }
            return type;
        }
    }
}


