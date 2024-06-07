using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UsuariosModel : BaseModel
    {
        public string nomeUsuario { get; set; }
        public string senha { get; set; }

    }

}
