using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class EmpresaDto
    {

        public int Id { get; set; }

        public string? Image { get; set; }

        [Display(Name = "Razon Social")]
        public string? Razonsocial { get; set; }

        [RegularExpression(@"^[0-9]*", ErrorMessage = "Debe contener unicamente numeros.")]
        [MaxLength(12)]
        [MinLength(12)]
        public string? RUT { get; set; }

        public int PlanId { get; set; }

    }
}
