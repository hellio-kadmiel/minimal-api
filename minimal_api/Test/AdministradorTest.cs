using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominio.Entidades;

namespace TestNovo;

[TestClass]
public class AdministradorTests
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adm = new Administrador();

        // Act
        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Senha = "teste123";
        adm.Perfil = "admin";

        // Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("teste@teste.com", adm.Email);
        Assert.AreEqual("teste123", adm.Senha);
        Assert.AreEqual("admin", adm.Perfil);
    }

    [TestMethod]
    public void TestarConstrutorPadrao()
    {
        // Arrange & Act
        var adm = new Administrador();

        // Assert
        Assert.IsNotNull(adm);
        Assert.AreEqual(0, adm.Id);
        Assert.IsNull(adm.Email);
        Assert.IsNull(adm.Senha);
        Assert.IsNull(adm.Perfil);
    }
}