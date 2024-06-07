using Application.Applications;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tests.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IUsuariosApp _usuariosApp;
        public LoginController(IUsuariosApp usuariosApp)
        {
            _usuariosApp = usuariosApp;
        }

        [HttpPost]
        [Route("LoginUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginModel login)
        {
            try
            {
                if (login == null || string.IsNullOrEmpty(login.nomeUsuario) || string.IsNullOrEmpty(login.senha))
                {
                    return Unauthorized("Parametros Invalidos");
                }

                login.senha = Cryptography.ConvertToSha256Hash(login.senha).ToLower();

                var user = await _usuariosApp.FindByAsync(x => x.nomeUsuario == login.nomeUsuario && x.senha == login.senha);

                if (user == null)
                {
                    return Unauthorized("Usuário ou Senha Invalidos");
                }

                AuthModel authModel = new AuthModel
                {
                    id = user.id,
                    nomeUsuario = user.nomeUsuario,
                    senha = user.senha,

                };


                var tokenJWT = TokenAuthentication.GenerateToken(authModel);

                return Ok(new LoginResponseModel
                {
                    data = user,
                    token = tokenJWT,
                });
            }
            catch (System.Exception)
            {
                return BadRequest("Erro Inesperado");
            }


        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuarios usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return Unauthorized("Parametros Invalidos");
                }


                if (await _usuariosApp.AnyAsync(x => x.nomeUsuario == usuarios.nomeUsuario))
                {
                    return BadRequest("Usuário Já Cadastrado");
                }

                usuarios.senha = Cryptography.ConvertToSha256Hash(usuarios.senha).ToLower();


                await _usuariosApp.Add(usuarios);

                await _usuariosApp.SaveChangesAsync();

                return Ok(new RegistroResponseModel
                {
                    data = usuarios,
                    message = "Usuário cadastrado com sucesso.",
                });
            }
            catch (Exception er)
            {
                return BadRequest("Erro Inesperado:" + er.Message);
            }
        }        
    }
}
