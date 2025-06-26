using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Areas.Commands.Update
{
    public class AreaUpdateCmdHndlr : ICommandHandler<AreaUpdateCmd,bool>
    {
        private readonly IAreaRepository _areaRepository;

        public AreaUpdateCmdHndlr(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<bool> Handle(AreaUpdateCmd command) 
        {
            if (command.AreaDto.Id == null || command.AreaDto.Name == null) { return false; }

            var id = command.AreaDto.Id;
            var exists = await _areaRepository.GetById(id);
            if (exists == null) { return false; }

            var Area =command.AreaDto;
            await _areaRepository.Update(Area);
            return true;
        }
    }
}
