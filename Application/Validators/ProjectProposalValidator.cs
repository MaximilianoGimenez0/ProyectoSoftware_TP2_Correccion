using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Responses;

namespace Application.Validators
{
    public class ProjectProposalValidator
    {
        // Método que valida los campos de una propuesta de proyecto
        public static (bool EsValido, string? Mensaje) Validar(ProjectProposalDto project)
        {
            // Valida que los campos no estén vacíos o sean inválidos
            if (string.IsNullOrWhiteSpace(project.Title))
                return (false, "El título no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(project.Description))
                return (false, "La descripción no puede estar vacía.");

            if (project.EstimatedAmount <= 0)
                return (false, "El monto estimado debe ser mayor que cero.");

            if (project.EstimatedDuration <= 0)
                return (false, "La duración estimada debe ser mayor que cero.");

            if (project.Area <= 0)
                return (false, "El área debe ser un número entero válido.");

            if (project.Type <= 0)
                return (false, "El tipo debe ser un número entero válido.");

            if (project.CreateBy <= 0)
                return (false, "El creador debe ser un número entero válido.");

            return (true, null); //Si todo es valido
        }
    }
}
