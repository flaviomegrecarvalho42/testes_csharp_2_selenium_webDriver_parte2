using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoDarUmLance
    {
        private IWebDriver _driver;

        public AoDarUmLance(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void DadoLoginInteressadaDevePermitirAtualizarLanceAtual()
        {
            #region Arrange
            new LoginPageObject(_driver).AbrirPagina()
                                        .InformarEmail("fulano@example.org")
                                        .InformarSenha("123")
                                        .EfetuarLogin();

            var detalheLeilaoPageObject = new DetalheLeilaoPageObject(_driver);
            detalheLeilaoPageObject.AbrirPagina(1);
            #endregion

            #region Act
            detalheLeilaoPageObject.DarLance(300);
            #endregion

            #region Assert
            //Criar um Wait explícito
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));

            //Criar uma função que ficará verificando se a condição é true.
            bool iguais = wait.Until(driver => detalheLeilaoPageObject.LanceAtual == 300);
            Assert.True(iguais);
            #endregion
        }
    }
}
