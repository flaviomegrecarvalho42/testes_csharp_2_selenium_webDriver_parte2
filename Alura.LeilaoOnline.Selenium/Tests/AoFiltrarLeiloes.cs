using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using System.Collections.Generic;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoFiltrarLeiloes
    {
        private IWebDriver _driver;

        public AoFiltrarLeiloes(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void DadoLoginInteressadaAoFiltrarLeiloesDeveMostrarPainelDeResultado()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("123")
                                        .EfetuarLogin();
            #endregion

            #region Act
            var dashboardInteressadaPageObject = new DashboardInteressadaPageObject(_driver);
            dashboardInteressadaPageObject.Filtro
                                          .PesquisarLeiloes(new List<string> { "Arte", "Coleções" }, "", true);
            #endregion

            #region Assert
            Assert.Contains("Resultado da pesquisa", _driver.PageSource);
            #endregion
        }
    }
}
