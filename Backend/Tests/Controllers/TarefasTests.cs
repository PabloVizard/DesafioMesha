using API.Controllers;
using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tests.Models;
using Tests.Utils;

namespace Tests.Controllers
{
    public class TarefasTests
    {
        private readonly Mock<ITarefasApp> _tarefasAppMock;
        private readonly TarefasController _tarefasController;

        public TarefasTests()
        {
            _tarefasAppMock = new Mock<ITarefasApp>();
            _tarefasController = new TarefasController(_tarefasAppMock.Object);

        }
        
        [Fact]
        public async Task ObterPorId_Ok()
        {
            // Arrange
            int id = 1;
            var tarefa = new Tarefas() { id = 1, titulo = "Test Titulo", descricao = "Test Descrição", concluida = false };

            AuthenticateApi();

            _tarefasAppMock.Setup(x => x.FindAsync(tarefa.id)).ReturnsAsync(tarefa);

            // Act
            var result = await _tarefasController.ObterPorId(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tarefa, okResult.Value);
        }

        [Fact]
        public async Task ObterPorId_Invalid_TokenInvalid()
        {
            // Arrange
            int id = 1;
            var tarefa = new Tarefas() { id = 1, titulo = "Test Titulo", descricao = "Test Descrição", concluida = false };

            _tarefasAppMock.Setup(x => x.FindAsync(tarefa.id)).ReturnsAsync(tarefa);

            // Act
            var result = await _tarefasController.ObterPorId(id);


            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task ObterTodos_Ok()
        {
            // Arrange
            var tarefas = new List<Tarefas>
        {
            new Tarefas { id = 1, titulo = "Titulo Test 1", descricao = "Descrição Test 1", concluida = false, usuarioId = 3 },
            new Tarefas { id = 2, titulo = "Titulo Test 2", descricao = "Descrição Test 2", concluida = false, usuarioId = 3 },
            new Tarefas { id = 3, titulo = "Titulo Test 3", descricao = "Descrição Test 3", concluida = false, usuarioId = 3 }
        };
            AuthenticateApi();
            _tarefasAppMock.Setup(x => x.ListAsync()).ReturnsAsync(tarefas);


            // Act
            var result = await _tarefasController.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var retorno = Assert.IsType<List<Tarefas>>(okResult.Value);
            Assert.Equal(3, retorno.Count);
        }
        [Fact]
        public async Task ObterTodos_Invalid_TokenInvalid()
        {
            // Arrange
            var tarefas = new List<Tarefas>
        {
            new Tarefas { id = 1, titulo = "Titulo Test 1", descricao = "Descrição Test 1", usuarioId = 0 },
            new Tarefas { id = 2, titulo = "Titulo Test 2", descricao = "Descrição Test 2", usuarioId = 0 },
            new Tarefas { id = 3, titulo = "Titulo Test 3", descricao = "Descrição Test 3", usuarioId = 0 }
        };
            _tarefasAppMock.Setup(x => x.ListAsync()).ReturnsAsync(tarefas);

            // Act
            var result = await _tarefasController.ObterTodos();

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Atualizar_Ok()
        {
            // Arrange
            var tarefaModel = new TarefasModel { id = 1, titulo = "Titulo Test Atualizado", descricao = "Descrição Test Atualizada" };
            var tarefa = new Tarefas { id = 1, titulo = "Titulo Test Antigo", descricao = "Descrição Test Antiga" };
            AuthenticateApi();

            _tarefasAppMock.Setup(x => x.FindAsync(1)).ReturnsAsync(tarefa);

            // Act
            var result = await _tarefasController.Atualizar(tarefaModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var retorno = Assert.IsType<Tarefas>(okResult.Value);
            Assert.Equal(tarefaModel.titulo, retorno.titulo);
            Assert.Equal(tarefaModel.descricao, retorno.descricao);
        }
        [Fact]
        public async Task Atualizar_Invalid_TokenInvalid()
        {
            // Arrange
            var tarefaModel = new TarefasModel { id = 1, titulo = "Titulo Test Atualizado", descricao = "Descrição Test Atualizada" };
            var tarefa = new Tarefas { id = 1, titulo = "Titulo Test Antigo", descricao = "Descrição Test Antiga" };

            _tarefasAppMock.Setup(x => x.FindAsync(1)).ReturnsAsync(tarefa);

            // Act
            var result = await _tarefasController.Atualizar(tarefaModel);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
        [Fact]
        public async Task Atualizar_Invalid_ObjectNotExist()
        {
            // Arrange
            var tarefaModel = new TarefasModel { id = 1, titulo = "Titulo Test Atualizado", descricao = "Descrição Test Atualizada" };
            AuthenticateApi();
            _tarefasAppMock.Setup(x => x.FindAsync(1)).ReturnsAsync((Tarefas)null);

            // Act
            var result = await _tarefasController.Atualizar(tarefaModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dados não existente", badRequestResult.Value);
        }

        [Fact]
        public async Task Registrar_Ok()
        {
            // Arrange
            var tarefa = new Tarefas { id = 0, concluida = false, usuarioId = 0, titulo = "New Titulo Test", descricao = "Nova Descrição Test" };
            AuthenticateApi();

            _tarefasAppMock.Setup(x => x.Add(It.IsAny<Tarefas>())).ReturnsAsync(tarefa);

            // Act
            var result = await _tarefasController.Registrar(tarefa);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var retorno = Assert.IsType<Tarefas>(okResult.Value);
            Assert.Equal(tarefa, retorno);
        }
        [Fact]
        public async Task Registrar_Invalid_TokenInvalid()
        {
            // Arrange
            var tarefa = new Tarefas { titulo = "New Titulo Test", descricao = "Nova Descrição Test" };
            _tarefasAppMock.Setup(x => x.Add(It.IsAny<Tarefas>())).ReturnsAsync(tarefa);

            // Act
            var result = await _tarefasController.Registrar(tarefa);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Remover_Ok()
        {
            // Arrange
            var tarefa = new Tarefas { id = 1, titulo = "Titulo Test", descricao = "Descrição Test" };
            _tarefasAppMock.Setup(x => x.Find(1)).Returns(tarefa);
            AuthenticateApi();

            // Act
            var result = await _tarefasController.Remover(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var retorno = Assert.IsType<BaseResponseModel<Tarefas>>(okResult.Value);

            Assert.Equal(tarefa.id, retorno.data.id);
        }
        [Fact]
        public async Task Remover_Invalid_TokenInvalid()
        {
            // Arrange
            var tarefa = new Tarefas { id = 1, titulo = "Titulo Test", descricao = "Descrição Test" };
            _tarefasAppMock.Setup(x => x.Find(1)).Returns(tarefa);

            // Act
            var result = await _tarefasController.Remover(1);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        public void AuthenticateApi()
        {

            var httpContext = FakeToken.MockHttpContext();

            _tarefasController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }
    }
}
