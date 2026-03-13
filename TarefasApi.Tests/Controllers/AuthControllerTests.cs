using Microsoft.AspNetCore.Mvc;
using Moq;
using TarefasApi.Controllers;
using TarefasApi.DTOs;
using TarefasApi.Services;
using Xunit;
using FluentAssertions;

namespace TarefasApi.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Registrar_DeveRetornarOk_QuandoSucesso()
        {
            // Arrange
            var dto = new LoginRequestDTO { Email = "teste@email.com", Senha = "123" };
            var response = new LoginResponseDTO { Email = dto.Email, Token = "token-valido" };
            
            _authServiceMock
                .Setup(x => x.Registrar(dto))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Registrar(dto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task Registrar_DeveRetornarConflict_QuandoEmailJaExiste()
        {
            // Arrange
            var dto = new LoginRequestDTO { Email = "existente@email.com", Senha = "123" };
            
            _authServiceMock
                .Setup(x => x.Registrar(dto))
                .ReturnsAsync((LoginResponseDTO)null);

            // Act
            var result = await _controller.Registrar(dto);

            // Assert
            result.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public async Task Login_DeveRetornarOk_QuandoCredenciaisValidas()
        {
            // Arrange
            var dto = new LoginRequestDTO { Email = "teste@email.com", Senha = "123" };
            var response = new LoginResponseDTO { Email = dto.Email, Token = "token-valido" };
            
            _authServiceMock
                .Setup(x => x.Login(dto))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task Login_DeveRetornarUnauthorized_QuandoCredenciaisInvalidas()
        {
            // Arrange
            var dto = new LoginRequestDTO { Email = "errado@email.com", Senha = "321" };
            
            _authServiceMock
                .Setup(x => x.Login(dto))
                .ReturnsAsync((LoginResponseDTO)null);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }
    }
}
