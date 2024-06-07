using API.Controllers;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Tests.Models;

namespace Tests
{
    public class LoginTests
    {
        private readonly Mock<IUsuariosApp> _usuariosAppMock;
        private readonly LoginController _loginController;

        public LoginTests()
        {
            _usuariosAppMock = new Mock<IUsuariosApp>();
            _loginController = new LoginController(_usuariosAppMock.Object);
        }

        [Fact]
        public async Task LoginUsuario_OK()
        {
            // Arrange
            var login = new LoginModel { nomeUsuario = "test", senha = "123" };
            var user = new Usuarios { id = 1, nomeUsuario = "test", senha = Cryptography.ConvertToSha256Hash("123").ToLower() };

            _usuariosAppMock.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Usuarios, bool>>>()))
                            .ReturnsAsync(user);

            // Act
            var result = await _loginController.LoginUsuario(login);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponseModel>(okResult.Value);
            Assert.Equal(user, response.data);
            Assert.NotNull(response.token);
        }
        [Fact]
        public async Task LoginUsuario_Invalid_WrongPassword()
        {
            // Arrange
            var login = new LoginModel { nomeUsuario = "test", senha = "123" };

            _usuariosAppMock.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Usuarios, bool>>>()))
                            .ReturnsAsync((Usuarios)null);

            // Act
            var result = await _loginController.LoginUsuario(login);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Usuário ou Senha Invalidos", unauthorizedResult.Value);
        }
        [Fact]
        public async Task LoginUsuario_Invalid_LoginIsNull()
        {
            // Act
            var result = await _loginController.LoginUsuario(null);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Parametros Invalidos", unauthorizedResult.Value);
        }
        [Fact]
        public async Task RegistrarUsuario_Ok()
        {
            // Arrange
            var usuarios = new Usuarios { nomeUsuario = "test", senha = "password" };
            _usuariosAppMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Usuarios, bool>>>()))
                            .ReturnsAsync(false);

            // Act
            var result = await _loginController.RegistrarUsuario(usuarios);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<RegistroResponseModel>(okResult.Value);
            Assert.Equal(usuarios, response.data);
            Assert.Equal("Usuário cadastrado com sucesso.", response.message);
        }
        [Fact]
        public async Task RegistrarUsuario_Invalid_UsuarioIsNull()
        {
            // Act
            var result = await _loginController.RegistrarUsuario(null);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Parametros Invalidos", unauthorizedResult.Value);
        }
        [Fact]
        public async Task RegistrarUsuario_Invalid_ExistingUser()
        {
            // Arrange
            var usuarios = new Usuarios { nomeUsuario = "test", senha = "123" };

            _usuariosAppMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Usuarios, bool>>>()))
                            .ReturnsAsync(true);

            // Act
            var result = await _loginController.RegistrarUsuario(usuarios);
            
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Usuário Já Cadastrado", badRequestResult.Value);
        }
        
    }
}