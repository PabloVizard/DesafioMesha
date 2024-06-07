using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefasController : BaseController<Tarefas, TarefasModel>
    {
        private readonly ITarefasApp _tarefasApp;
        public TarefasController(ITarefasApp tarefasApp) : base(tarefasApp)
        {
            _tarefasApp = tarefasApp;
        }

        [HttpGet]
        [Route("ObterTodos")]
        public override async Task<IActionResult> ObterTodos()
        {
            try
            {
                AuthModel authModel;

                try
                {
                    authModel = await GetTokenAuthModelAsync();
                }
                catch (Exception ex)
                {
                    return Unauthorized("Erro ao obter token:" + ex.Message);
                }
                var tarefas = await _tarefasApp.ListAsync();

                var retorno = tarefas.Where(x => x.usuarioId == authModel.id).ToList();

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }
    }
}
