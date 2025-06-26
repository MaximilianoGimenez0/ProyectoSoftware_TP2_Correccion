using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ProjectTypes.Commands.Update
{
    public class ProjectTypeUpdateCmdHndlr : ICommandHandler<ProjectTypeUpdateCmd,bool>
    {
        private readonly IProjectTypeRepository _proyectTypeRepository;

        public ProjectTypeUpdateCmdHndlr(IProjectTypeRepository proyectTypeRepository)
        {
            _proyectTypeRepository = proyectTypeRepository;
        }

        public async Task<bool> Handle(ProjectTypeUpdateCmd command) 
        {
            if (command.ProjectTypeDto.Id == null || command.ProjectTypeDto.Name == null) { return false; }

            var id = command.ProjectTypeDto.Id;
            var exists = await _proyectTypeRepository.GetById(id);
            if (exists == null) { return false; }

            var ProjectType = command.ProjectTypeDto;
            await _proyectTypeRepository.Update(ProjectType);
            return true;
        }
    }
}
