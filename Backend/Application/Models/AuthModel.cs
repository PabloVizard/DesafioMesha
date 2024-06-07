using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AuthModel
    {
        public new int id { get; set; }
        public string nomeUsuario { get; set; } = "";
        public string senha { get; set; } = "";

    }
}
