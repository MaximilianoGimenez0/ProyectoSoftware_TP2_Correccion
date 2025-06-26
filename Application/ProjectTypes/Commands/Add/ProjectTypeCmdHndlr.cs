using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ProjectTypes.Commands.Add
{
    public class ProjectTypeAddCmdHndlr : ICommandHandler<ProjectTypeAddCmd,bool>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;
        public ProjectTypeAddCmdHndlr(IProjectTypeRepository projectTypeRepository) 
        {
            _projectTypeRepository = projectTypeRepository; 
        }

        public async Task<bool> Handle(ProjectTypeAddCmd command) 
        {
            if (command.ProjectTypeDto.Name == null) { return false; }

            var ProjectType = command.ProjectTypeDto;
            await _projectTypeRepository.Add(ProjectType);
            return true;
        }
    }
}
