using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class TarefasApp : BaseApp<Tarefas, TarefasModel>, ITarefasApp
    {
        protected readonly ITarefasService _tarefasService;
        protected readonly IMapper _mapper;

        public TarefasApp(ITarefasService tarefasService, IMapper mapper) : base(tarefasService, mapper)
        {
            _tarefasService = tarefasService;
            _mapper = mapper;
        }
    }
}
