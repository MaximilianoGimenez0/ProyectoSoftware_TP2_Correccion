using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.Queries.GetAll;
using Application.Dtos;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ProjectTypes.Queries.GetAll;
using Application.Users.Queries.GetAll;
using Domain.Entities;

namespace Application.Services
{
    public class ProjectTypeService : IProjectTypeService
    {
        private readonly IQueryHandler<ProjectTypeGetAllQry, List<ProjectType>> _projectTypeGetAllHndlr;

        public ProjectTypeService(IQueryHandler<ProjectTypeGetAllQry, List<ProjectType>> projectTypeGetAllHndlr)
        {
            _projectTypeGetAllHndlr = projectTypeGetAllHndlr;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var results = new List<GenericResponse>();
            var types = await _projectTypeGetAllHndlr.Handle(new ProjectTypeGetAllQry());
            foreach (var type in types)
            {
                var tempType = new GenericResponse() { id = type.Id, name = type.Name };
                results.Add(tempType);
            }

            return results;
        }
    }
}
