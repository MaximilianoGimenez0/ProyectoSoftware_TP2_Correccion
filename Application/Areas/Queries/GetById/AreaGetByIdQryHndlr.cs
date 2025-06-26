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

namespace Application.Areas.Queries.GetById
{
    public class AreaGetByIdQryHndlr : IQueryHandler<AreaGetByIdQry, Area?>
    {
        private readonly IAreaRepository _areaRepository;

        public AreaGetByIdQryHndlr (IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<Area?> Handle(AreaGetByIdQry query)
        {
            if (query.AreaDto.Id == null) { return null; }

            var id = query.AreaDto.Id.Value;
            var area = await _areaRepository.GetById(id);

            if (area == null) { return null; }
            return area;
        }
    }
}
