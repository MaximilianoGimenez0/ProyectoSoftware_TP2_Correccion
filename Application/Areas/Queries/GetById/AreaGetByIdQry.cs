using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Areas.Queries.GetById
{
    public class AreaGetByIdQry
    {
        public AreaDto AreaDto { get; set; }

        public AreaGetByIdQry (AreaDto areaDto)
        {
            AreaDto = areaDto;
        }
    }
}
