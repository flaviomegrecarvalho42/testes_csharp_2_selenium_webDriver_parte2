using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoEfetuarLogin
    {
        private IWebDriver _driver;

        public AoEfetuarLogin(TestFixture fixture)
        {
            _driver = fixture.Driver;    
        }

        [Fact]
        public void DadoCredenciaisValidasDeveIrParaPaginaDeDashboard()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("123")
                                        .EfetuarLogin(); //Act
            #endregion

            #region Assert
            Assert.Contains("Dashboard", _driver.Title);
            #endregion
        }

        [Fact]
        public void DadoCrendenciasInvalidasDeveContinuarNaPaginaDeLogin()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("")
                                        .EfetuarLogin(); //Act
            #endregion

            #region Assert
            Assert.Contains("Login", _driver.PageSource);
            #endregion
        }
    }
}
