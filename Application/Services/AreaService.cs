using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.Queries.GetAll;
using Application.Dtos;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IQueryHandler<AreaGetAllQry, List<AreaDto>> _areaGetAllHndlr;

        public AreaService(IQueryHandler<AreaGetAllQry, List<AreaDto>> areaGetAllHndlr)
        {
            _areaGetAllHndlr = areaGetAllHndlr;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var results = new List<GenericResponse>();
            var areas = await _areaGetAllHndlr.Handle(new AreaGetAllQry());
            foreach (var area in areas) 
            {
                var tempArea = new GenericResponse() { id = area.Id.Value, name = area.Name };
                results.Add(tempArea);
            }

            return results;
        }
    }
}
