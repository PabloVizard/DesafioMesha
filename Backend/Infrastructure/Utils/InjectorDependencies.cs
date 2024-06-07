using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Domain.Services;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Applications.Interfaces;
using Application.Applications;

namespace Infrastructure.Utils
{
    public static class InjectorDependencies
    {
        public static void Registrer(IServiceCollection services)
        {
            #region Application

            services.AddScoped(typeof(IBaseApp<,>), typeof(BaseApp<,>));
            services.AddScoped<IUsuariosApp, UsuariosApp>();
            services.AddScoped<ITarefasApp, TarefasApp>();


            #endregion

            #region Services

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<ITarefasService, TarefasService>();


            #endregion

            #region Repositories 

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<ITarefasRepository, TarefasRepository>();


            #endregion
        }
    }
}
