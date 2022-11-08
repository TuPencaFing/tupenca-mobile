using System.ComponentModel.DataAnnotations;

namespace tupenca_mobile.Model.Dto
{
    public class UserDto
    {
        [Required]
        public string token { get; set; }
    }
}
