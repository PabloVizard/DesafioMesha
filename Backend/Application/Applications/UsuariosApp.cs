using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Services;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class UsuariosApp : BaseApp<Usuarios, UsuariosModel>, IUsuariosApp
    {
        protected readonly IUsuariosService _usuariosService;
        protected readonly IMapper _mapper;

        public UsuariosApp(IUsuariosService usuariosService, IMapper mapper) : base(usuariosService, mapper)
        {
            _usuariosService = usuariosService;
            _mapper = mapper;
        }
        public virtual void UpdateEntity(Usuarios usuarios)
        {
            _usuariosService.Update(usuarios);
        }
    }
}
