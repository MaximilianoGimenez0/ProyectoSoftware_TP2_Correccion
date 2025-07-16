using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Requests
{
    public class DecisionStepUpdateRequest
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        [Range(0, long.MaxValue, ErrorMessage = "El status debe ser un numero positivo.")]
        public long id {  get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El usuario debe ser un numero positivo.")]
        public int user { get; set; }

        [Required(ErrorMessage = "El status es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El status debe ser un numero positivo.")]
        public int status { get; set; }

        [StringLength(255, ErrorMessage = "La observación no puede tener más de 255 caracteres.")]
        public string observation { get; set; }
    }
}
