using Application.Models;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Models
{
    public class LoginResponseModel
    {
        public Usuarios data { get; set; }
        public string token { get; set; }
    }
    public class RegistroResponseModel
    {
        public Usuarios data { get; set; }
        public string message { get; set; }
    }
}
