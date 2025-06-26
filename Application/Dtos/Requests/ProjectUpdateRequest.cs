using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Requests
{
    public class ProjectUpdateRequest
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(255, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string? title { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(255, ErrorMessage = "La descripción no puede tener más de 100 caracteres.")]
        public string? description { get; set; }

        [Required(ErrorMessage = "La duración estimada es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La duración estimada debe ser un número positivo.")]
        public int? duration { get; set; }
    }
}
