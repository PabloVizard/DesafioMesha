using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BaseController<Entity, Model> : ControllerBase
        where Entity : BaseEntity
        where Model : BaseModel
    {
        protected readonly IBaseApp<Entity, Model> _baseApp;

        public BaseController(IBaseApp<Entity, Model> baseApp)
        {
            _baseApp = baseApp;
        }

        
        [HttpGet]
        [Route("ObterPorId")]
        public virtual async Task<IActionResult> ObterPorId(int id)
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

                return Ok(await _baseApp.FindAsync(id));
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpGet]
        [Route("ObterTodos")]
        public virtual async Task<IActionResult> ObterTodos()
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
                var retorno = await _baseApp.ListAsync();

                return Ok(retorno);
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public virtual async Task<IActionResult> Atualizar(Model dados)
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

                var dadosFind = await _baseApp.FindAsync(dados.id);
                if (dadosFind == null)
                {
                    return BadRequest("Dados não existente");
                }

                var dadosProperties = dados.GetType().GetProperties();
                var dadosFindProperties = dadosFind.GetType().GetProperties();

                foreach (var property in dadosProperties)
                {
                    var findProperty = dadosFindProperties.FirstOrDefault(p => p.Name == property.Name);
                    if (findProperty != null && property.GetValue(dados) != null)
                    {
                        findProperty.SetValue(dadosFind, property.GetValue(dados));
                    }
                }

                _baseApp.Update(dadosFind);
                await _baseApp.SaveChangesAsync();

                return Ok(dadosFind);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException &&
                    (sqlException.Number == 2601 || sqlException.Number == 2627))
                {
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");
            }


        }
        [HttpPost]
        [Route("Registrar")]
        public virtual async Task<IActionResult> Registrar(Entity dados)
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

                var data = await _baseApp.Add(dados);
                return Ok(data);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException &&
                    (sqlException.Number == 2601 || sqlException.Number == 2627))
                {
                    return BadRequest("O registro com o mesmo valor já existe. Por favor, verifique seus dados.");
                }

                return BadRequest("Erro Inesperado");
            }


        }

        [HttpDelete]
        [Route("Remover")]
        public virtual async Task<IActionResult> Remover(int id)
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

                var dados = _baseApp.Find(id);
                _baseApp.Remove(dados);
                await _baseApp.SaveChangesAsync();
                return Ok(new BaseResponseModel<Entity>
                {
                    data = dados,
                    message = "Removido com sucesso."
                });
            }
            catch (Exception ex)
            {

                return BadRequest("Erro Inesperado: " + ex.Message);
            }
            
        }
        protected async Task<string?> GetTokenAsync()
        {
            return await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
        }

        protected async Task<AuthModel> GetTokenAuthModelAsync()
        {
            return TokenAuthentication.GetTokenAuthModel(await GetTokenAsync());
        }

        protected string GenerateToken(AuthModel authModel)
        {
            return TokenAuthentication.GenerateToken(authModel);
        }
    }
}
