using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;
using minimal_api.Dominio.DTOs;

namespace Test;

[TestClass]
public class AdministradorServico
{
    private DbContexto CriarContextoTeste()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
            .Options;
        return new DbContexto(options);
    }

    [TestMethod]
    public void TestarIncluirAdministrador()
    {
        // Arrange
        using var contexto = CriarContextoTeste();
        var servico = new AdministradorServico(contexto);
        
        var administrador = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "teste123",
            Perfil = "editor"
        };

        // Act
        var resultado = servico.Incluir(administrador);

        // Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual("teste@teste.com", resultado.Email);
        Assert.AreEqual("teste123", resultado.Senha);
        Assert.AreEqual("editor", resultado.Perfil);
    }

    [TestMethod]
    public void TestarBuscarPorId()
    {
        // Arrange
        using var contexto = CriarContextoTeste();
        var servico = new AdministradorServico(contexto);
        
        var administrador = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "teste123",
            Perfil = "editor"
        };
        
        var adminInserido = servico.Incluir(administrador);

        // Act
        var resultado = servico.BuscarPorId(adminInserido.Id);

        // Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual(adminInserido.Id, resultado.Id);
        Assert.AreEqual("teste@teste.com", resultado.Email);
    }

    [TestMethod]
    public void TestarLoginComCredenciaisCorretas()
    {
        // Arrange
        using var contexto = CriarContextoTeste();
        var servico = new AdministradorServico(contexto);
        
        var administrador = new Administrador
        {
            Email = "admin@teste.com",
            Senha = "senha123",
            Perfil = "admin"
        };
        
        servico.Incluir(administrador);

        var loginDTO = new LoginDTO
        {
            Email = "admin@teste.com",
            Password = "senha123"
        };

        // Act
        var resultado = servico.Login(loginDTO);

        // Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual("admin@teste.com", resultado.Email);
    }

    [TestMethod]
    public void TestarLoginComCredenciaisIncorretas()
    {
        // Arrange
        using var contexto = CriarContextoTeste();
        var servico = new AdministradorServico(contexto);
        
        var administrador = new Administrador
        {
            Email = "admin@teste.com",
            Senha = "senha123",
            Perfil = "admin"
        };
        
        servico.Incluir(administrador);

        var loginDTO = new LoginDTO
        {
            Email = "admin@teste.com",
            Password = "senha_errada"
        };

        // Act
        var resultado = servico.Login(loginDTO);

        // Assert
        Assert.IsNull(resultado);
    }
}