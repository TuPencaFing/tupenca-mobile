﻿using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class CampeonatoDto
    {
        
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public DeporteDto? Deporte { get; set; }

        public List<EventoDto> Eventos { get; set; } = new List<EventoDto>();

    }
}

