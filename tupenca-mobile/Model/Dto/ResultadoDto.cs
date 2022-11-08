using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class ResultadoDto
    {
        public TipoResultado resultado { get; set; }

        public int? PuntajeEquipoLocal { get; set; }

        public int? PuntajeEquipoVisitante { get; set; }

        public int EventoId { get; set; }

    }

    public enum TipoResultado
    {
        Empate,
        VictoriaEquipoLocal,
        VictoriaEquipoVisitante
    }
}
