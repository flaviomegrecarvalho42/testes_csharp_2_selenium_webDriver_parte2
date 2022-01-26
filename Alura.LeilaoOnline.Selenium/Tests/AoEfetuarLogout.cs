using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoEfetuarLogout
    {
        private IWebDriver _driver;

        public AoEfetuarLogout(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void DadoLoginValidoDeveIrParaHomeNaoLogada()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("123")
                                        .EfetuarLogin();
            #endregion

            #region Act
            DashboardInteressadaPageObject dashboardPageObject = new DashboardInteressadaPageObject(_driver);
            dashboardPageObject.Menu.EfetuarLogout();
            #endregion

            #region Assert
            Assert.Contains("Próximos Leilões", _driver.PageSource);
            #endregion
        }
    }
}
