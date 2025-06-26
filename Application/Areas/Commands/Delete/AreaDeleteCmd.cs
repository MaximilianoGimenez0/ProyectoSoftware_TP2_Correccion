using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Areas.Commands.Delete
{
    public class AreaDeleteCmd
    {
        public AreaDto AreaDto { get; set; }

        public AreaDeleteCmd(AreaDto areaDto) { AreaDto = areaDto; }
    }
}
