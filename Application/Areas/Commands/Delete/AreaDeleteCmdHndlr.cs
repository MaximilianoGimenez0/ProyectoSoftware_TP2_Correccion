using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Areas.Commands.Delete
{
    public class AreaDeleteCmdHndlr : ICommandHandler<AreaDeleteCmd,bool>
    {
        private readonly IAreaRepository _areaRepository;

        public AreaDeleteCmdHndlr (IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<bool> Handle(AreaDeleteCmd command) 
        {
            if (command.AreaDto.Id == null) { return false; }
            var id = command.AreaDto.Id.Value;

            
            var exists = await _areaRepository.GetById(id);
            if (exists == null) { return false; }

            return await _areaRepository.Delete(id);
        }
    }
}
