using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Areas.Commands.Add
{
    public class AreaAddCmdHndlr : ICommandHandler<AreaAddCmd,bool>
    {
        private readonly IAreaRepository _areaRepository;
        public AreaAddCmdHndlr(IAreaRepository areaRepository) 
        {
            _areaRepository = areaRepository; 
        }

        public async Task<bool> Handle(AreaAddCmd command) 
        {
            if (command.AreaDto.Name == null) { return false; }

            var Area = command.AreaDto;
            await _areaRepository.Add(Area);
            return true;
        }
    }
}
