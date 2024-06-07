using Application.Models;
using AutoMapper;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {
            CreateMap<Usuarios, UsuariosModel>().ReverseMap();
            CreateMap<Tarefas, TarefasModel>().ReverseMap();

        }
    }
}
