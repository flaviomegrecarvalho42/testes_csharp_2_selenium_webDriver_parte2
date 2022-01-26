using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoNavegarParaFormularioNovoLeilao
    {
        private IWebDriver _driver;

        public AoNavegarParaFormularioNovoLeilao(TestFixture fixture)
        {
            this._driver = fixture.Driver;
        }

        [Fact]
        public void DadoLoginAdministrativoDeveMostrarTodasAsCategorias()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("123")
                                        .EfetuarLogin();
            #endregion

            #region Act
            var novoLeilaoPageObject = new NovoLeilaoPageObject(_driver);
            novoLeilaoPageObject.AbrirPagina();
            #endregion

            #region Assert
            Assert.Equal(3, novoLeilaoPageObject.Categorias.Count());
            #endregion
        }
    }
}
