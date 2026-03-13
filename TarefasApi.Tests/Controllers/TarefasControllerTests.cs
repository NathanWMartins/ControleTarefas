using Microsoft.AspNetCore.Mvc;
using Moq;
using TarefasApi.Controllers;
using TarefasApi.DTOs;
using TarefasApi.Services;
using Xunit;
using FluentAssertions;

namespace TarefasApi.Tests.Controllers
{
    public class TarefasControllerTests
    {
        private readonly Mock<ITarefasService> _serviceMock;
        private readonly TarefasController _controller;

        public TarefasControllerTests()
        {
            _serviceMock = new Mock<ITarefasService>();
            _controller = new TarefasController(_serviceMock.Object);
        }

        [Fact]
        public async Task Get_DeveRetornarOk_ComListaDeTarefas()
        {
            // Arrange
            var tarefas = new List<TarefaResponseDTO> 
            { 
                new TarefaResponseDTO { Id = 1, Descricao = "Tarefa 1" } 
            };
            _serviceMock
                .Setup(x => x.GetTarefas(1, 10))
                .ReturnsAsync((tarefas, 1));

            // Act
            var result = await _controller.Get(1, 10);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Criar_DeveRetornarOk_ComNovaTarefa()
        {
            // Arrange
            var dto = new TarefaRequestDTO { Descricao = "Nova Tarefa" };
            var response = new TarefaResponseDTO { Id = 1, Descricao = "Nova Tarefa" };
            
            _serviceMock
                .Setup(x => x.CriarTarefa(dto))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Criar(dto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task GetPorId_DeveRetornarOk_QuandoExiste()
        {
            // Arrange
            var response = new TarefaResponseDTO { Id = 1, Descricao = "Tarefa 1" };
            _serviceMock
                .Setup(x => x.GetTarefaPorId(1))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetPorId(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task GetPorId_DeveRetornarNotFound_QuandoNaoExiste()
        {
            // Arrange
            _serviceMock
                .Setup(x => x.GetTarefaPorId(1))
                .ReturnsAsync((TarefaResponseDTO)null);

            // Act
            var result = await _controller.GetPorId(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Concluir_DeveRetornarOk_QuandoSucesso()
        {
            // Arrange
            var response = new TarefaResponseDTO { Id = 1, Descricao = "Tarefa 1", Concluida = true };
            _serviceMock
                .Setup(x => x.ConcluirTarefa(1))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Concluir(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task Excluir_DeveRetornarNoContent_QuandoSucesso()
        {
            // Arrange
            _serviceMock
                .Setup(x => x.ExcluirTarefa(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Excluir(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Excluir_DeveRetornarNotFound_QuandoNaoExiste()
        {
            // Arrange
            _serviceMock
                .Setup(x => x.ExcluirTarefa(1))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Excluir(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
