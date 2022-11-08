using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class ResultadoUpdateDto
    {
        [Required]
        public TipoResultado resultado { get; set; }

        public int? PuntajeEquipoLocal { get; set; }

        public int? PuntajeEquipoVisitante { get; set; }
    }
}
