using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoEfetuarRegistro
    {
        private IWebDriver _driver;

        public AoEfetuarRegistro(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void DadoInformacoesValidasDeveIrParaPaginaDeAgradecimento()
        {
            #region Arrange
            var registroPageObject = new RegistroPageObject(_driver);
            registroPageObject.AbrirPagina();
            registroPageObject.PreencherFormulario("Flavio Megre", "flavio.megre@uol.com.br", "123", "123");
            #endregion

            #region Act
            registroPageObject.SubmeterFormulario();
            #endregion

            #region Assert
            Assert.Contains("Obrigado", _driver.PageSource);
            #endregion
        }

        [Theory]
        [InlineData("", "flavio.megre@uol.com.br", "123", "123")]
        [InlineData("Flavio Megre", "flavio", "123", "123")]
        [InlineData("Flavio Megre", "flavio.megre@uol.com.br", "123", "456")]
        [InlineData("Flavio Megre", "flavio.megre@uol.com.br", "123", "")]
        public void DadoInformacoesInvalidasDeveContinuarNaHomePage(string nome,
                                                         string email,
                                                         string senha,
                                                         string confirmSenha)
        {
            #region Arrange
            var registroPageObject = new RegistroPageObject(_driver);
            registroPageObject.AbrirPagina();
            registroPageObject.PreencherFormulario(nome, email, senha, confirmSenha);
            #endregion

            #region Act
            registroPageObject.SubmeterFormulario();
            #endregion

            #region Assert
            Assert.Contains("section-registro", _driver.PageSource);
            #endregion
        }

        [Fact]
        public void DadoNomeEmBrancoDeveMostrarMensagemDeErro()
        {
            #region Arrange
            var registroPageObject = new RegistroPageObject(_driver);
            registroPageObject.AbrirPagina();
            #endregion

            #region Act
            registroPageObject.SubmeterFormulario();
            #endregion

            #region Assert
            Assert.Equal("The Nome field is required.", registroPageObject.MensagemErrorNome);
            #endregion
        }

        [Fact]
        public void DadoEmailInvalidoDeveMostrarMensagemDeErro()
        {
            #region Arrange
            var registroPageObject = new RegistroPageObject(_driver);
            registroPageObject.AbrirPagina();

            registroPageObject.PreencherFormulario(nome: "",
                                                   email: "flavio",
                                                   senha: "",
                                                   confirmSenha: "");
            #endregion

            #region Act
            registroPageObject.SubmeterFormulario();
            #endregion

            #region Assert
            Assert.Equal("Please enter a valid email address.", registroPageObject.MensagemErrorEmail);
            #endregion
        }
    }
}
