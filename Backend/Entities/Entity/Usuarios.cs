﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Usuarios : BaseEntity
    {
        public string nomeUsuario { get; set; }

        public string senha { get; set; }
    }
}
