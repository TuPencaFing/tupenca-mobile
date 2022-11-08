using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class PlanDto
    {

        public int Id { get; set; }

        public int CantUser { get; set; }

        public decimal PercentageCost { get; set; }

        public int LookAndFeel { get; set; }

    }
}

