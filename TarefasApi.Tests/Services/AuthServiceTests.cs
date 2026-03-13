using Xunit;
using Moq;
using FluentAssertions;
using TarefasApi.Services;
using TarefasApi.Repositories;
using Microsoft.Extensions.Configuration;
using TarefasApi.DTOs;
using TarefasApi.Models;

public class AuthServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _usuarioRepoMock = new Mock<IUsuarioRepository>();

        var inMemorySettings = new Dictionary<string, string?>
        {
            {"Jwt:Key", "minha_chave_super_secreta_123456"},
            {"Jwt:Issuer", "ApiEstudo"},
            {"Jwt:Audience", "ApiEstudoUsers"},
            {"Jwt:ExpiracaoEmHoras", "2"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _authService = new AuthService(
            _usuarioRepoMock.Object,
            configuration
        );
    }

    [Fact]
    public async Task Registrar_DeveCriarUsuario_QuandoEmailNaoExiste()
    {
        // Arrange
        var dto = new LoginRequestDTO
        {
            Email = "teste@email.com",
            Senha = "123456"
        };

        _usuarioRepoMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync((Usuario)null);

        // Act
        var resultado = await _authService.Registrar(dto);

        // Assert
        resultado.Should().NotBeNull();

        _usuarioRepoMock.Verify(
            x => x.AddAsync(It.IsAny<Usuario>()),
            Times.Once
        );
    }

    [Fact]
    public async Task Registrar_DeveRetornarNull_QuandoEmailJaExiste()
    {
        // Arrange
        var dto = new LoginRequestDTO
        {
            Email = "teste@email.com",
            Senha = "123456"
        };

        _usuarioRepoMock
            .Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync(new Usuario());

        // Act
        var resultado = await _authService.Registrar(dto);

        // Assert
        resultado.Should().BeNull();
    }
}