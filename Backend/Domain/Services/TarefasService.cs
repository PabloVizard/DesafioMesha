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

    public class TarefasService : BaseService<Tarefas>, ITarefasService
    {
        private readonly ITarefasRepository _tarefasRepository;
        private readonly IMapper _mapper;
        public TarefasService(ITarefasRepository tarefasRepository, IMapper mapper) : base(tarefasRepository, mapper)
        {
            _tarefasRepository = tarefasRepository;
            _mapper = mapper;
        }
    }
}
