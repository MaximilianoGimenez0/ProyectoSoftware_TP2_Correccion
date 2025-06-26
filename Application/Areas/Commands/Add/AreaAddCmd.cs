using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.Areas.Commands.Add
{
    public class AreaAddCmd
    {
        public Area AreaDto { get; set; }

        public AreaAddCmd(Area areaDto)
        {
            AreaDto = areaDto;
        }
    }
}
