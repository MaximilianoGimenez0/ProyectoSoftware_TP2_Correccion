using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Areas.Queries.GetAll
{
    public class AreaGetAllQryHndlr : IQueryHandler<AreaGetAllQry,List<AreaDto>>
    {
        private readonly IAreaRepository _areaRepository;

        public AreaGetAllQryHndlr(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<List<AreaDto>> Handle(AreaGetAllQry query)
        {
            var areas = await _areaRepository.GetAll(); // lista de entidades EF

            var dtoAreas = new List<Application.Dtos.AreaDto>();


            foreach (var area in areas)
            {
                var temp = new Application.Dtos.AreaDto()
                {
                    Name = area.Name,
                    Id = area.Id,
                };
                dtoAreas.Add(temp);
            }

            return dtoAreas;
        }

     
    }
}
