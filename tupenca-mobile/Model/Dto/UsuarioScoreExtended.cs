using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupenca_mobile.Model.Dto
{
    public class UsuarioScoreExtended
    {
        public PuntajeUsuarioPencaDto usuarioScore { get; set; }

        public int Position { get; set; }

        public Color Color { get; set; } = Colors.White;

    }
}
