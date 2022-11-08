using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class PremioDto
    {

        public int Id { get; set; }

        public string? Image { get; set; }

        public int Position { get; set; }

        public decimal Percentage { get; set; }

    }
}

