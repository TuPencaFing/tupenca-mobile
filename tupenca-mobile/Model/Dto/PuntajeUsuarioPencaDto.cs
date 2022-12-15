using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_mobile.Model.Dto
{
    public class PuntajeUsuarioPencaDto
    {

        public int Id { get; set; }

        public int PencaId { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioDto Usuario { get; set; }

        public int Score { get; set; }

    }
}
