using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoCriarLeilao
    {
        private IWebDriver _driver;

        public AoCriarLeilao(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void DadoLoginAdministrativoEInformacoesValidasNoFormularioDeveCadastrarLeilao()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("123")
                                        .EfetuarLogin();

            var novoLeilaoPageObject = new NovoLeilaoPageObject(_driver);
            novoLeilaoPageObject.AbrirPagina();
            novoLeilaoPageObject.PreencherFormulario("Leilão Categoria 1",
                                                     "Nullam aliquet condimentum elit vitae volutpat. Vivamus ut nisi venenatis, facilisis odio eget, lobortis tortor. Cras mattis sit amet dolor id bibendum. Nulla turpis justo, porttitor eget leo vel, dictum tempor diam. Sed dui arcu, feugiat nec placerat ac, suscipit a mi. Etiam eget risus et tellus placerat tincidunt at ut lorem.",
                                                     "Item de Colecionador 1",
                                                     45000,
                                                     "C:\\images\\colecao1.jpg",
                                                     DateTime.Now.AddDays(10),
                                                     DateTime.Now.AddDays(40));
            #endregion

            #region Act
            novoLeilaoPageObject.SubmeterFormulario();
            #endregion

            #region Assert
            Assert.Contains("Leilões cadastrados no sistema", _driver.PageSource);
            #endregion
        }
    }
}
