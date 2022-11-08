﻿using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class PencaCompartidaDto
    {

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public CampeonatoDto? Campeonato { get; set; }

        public List<PremioDto>? Premios { get; set; } = new List<PremioDto>();

        public int PuntajeId { get; set; }
        public virtual PuntajeDto? Puntaje { get; set; }

        public decimal CostEntry { get; set; }

        public decimal Pozo { get; set; }

        public decimal Commission { get; set; }

    }
}

