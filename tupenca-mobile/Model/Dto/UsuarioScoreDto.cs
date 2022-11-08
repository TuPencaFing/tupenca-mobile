using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_mobile.Model.Dto;

namespace tupenca_mobile.Model.Dto
{
    public class UsuarioScoreDTO
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public int TotalScore { get; set; }

    }
}
