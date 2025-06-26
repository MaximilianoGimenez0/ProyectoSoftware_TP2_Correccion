using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.ProjectTypes.Queries.GetAll
{
    public class ProjectTypeGetAllQryHndlr : IQueryHandler<ProjectTypeGetAllQry,List<ProjectType>>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        public ProjectTypeGetAllQryHndlr(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        public async Task<List<ProjectType>> Handle(ProjectTypeGetAllQry query)
        {
            var projectTypes = await _projectTypeRepository.GetAll(); 

            var projectTypeDtos = new List<ProjectType>();


            foreach (var projectType in projectTypes)
            {
                var temp = new ProjectType()
                {
                    Name = projectType.Name,
                    Id = projectType.Id,
                };
                projectTypeDtos.Add(temp);
            }

            return projectTypeDtos;
        }
    }
}
