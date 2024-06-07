using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Tarefas : BaseEntity
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public bool concluida { get; set; }
        public int usuarioId { get; set; }

    }
}
