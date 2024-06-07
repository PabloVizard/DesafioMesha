using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TarefasModel : BaseModel
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public bool concluida { get; set; }
        public int usuarioId { get; set; }
    }

    public class TarefasResponseModel
    {
        public TarefasModel data { get; set; }
        public string message { get; set; }
    }
}
