using AutoMapper;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UsuariosService : BaseService<Usuarios>, IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IMapper _mapper;
        public UsuariosService(IUsuariosRepository usuariosRepository, IMapper mapper) : base(usuariosRepository, mapper)
        {
            _usuariosRepository = usuariosRepository;
            _mapper= mapper;
        }
    }
}
