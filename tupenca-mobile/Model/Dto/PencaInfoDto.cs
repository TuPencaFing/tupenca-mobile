using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class PencaInfoDto
    {

        public int Id { get; set; }

        public string? PencaTitle { get; set; }

        public string? PencaDescription { get; set; }

        public string? Image { get; set; }

        public decimal? Pozo { get; set; }

        public string? CampeonatoName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public DeporteDto? Deporte { get; set; }

        public List<EventoPrediccionDto> Eventos { get; set; } = new List<EventoPrediccionDto>();

        public int? PuntajeTotal { get; set; }

    }
}

