using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Responses;

namespace Application.Interfaces.Services
{
    public interface IAreaService
    {
        Task<List<GenericResponse>> GetAll();
    }
}
