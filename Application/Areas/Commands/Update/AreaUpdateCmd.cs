using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.Areas.Commands.Update
{
    public class AreaUpdateCmd
    {
        public Area AreaDto { get; set; }

        public AreaUpdateCmd(Area areaDto) 
        {
            AreaDto = areaDto;
        }
    }
}
