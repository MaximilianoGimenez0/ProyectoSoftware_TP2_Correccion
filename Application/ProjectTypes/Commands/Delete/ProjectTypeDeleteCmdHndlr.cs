using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ProjectTypes.Commands.Delete
{
    public class ProjectTypeDeleteCmdHndlr : ICommandHandler<ProjectTypeDeleteCmd,bool>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        public ProjectTypeDeleteCmdHndlr(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        public async Task<bool> Handle(ProjectTypeDeleteCmd command) 
        {
            if (command.PrjectTypeDto.Id == null) { return false; }
            var id = command.PrjectTypeDto.Id.Value;

            var exists = await _projectTypeRepository.GetById(id);
            if (exists == null) { return false; }

            return await _projectTypeRepository.Delete(id);
        }
    }
}
