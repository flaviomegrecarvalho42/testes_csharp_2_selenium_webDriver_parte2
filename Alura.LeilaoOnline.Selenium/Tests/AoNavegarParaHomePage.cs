using Alura.LeilaoOnline.Selenium.Fixtures;
using OpenQA.Selenium;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoNavegarParaHomePage
    {
        private IWebDriver _driver;

        public AoNavegarParaHomePage(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void DadoChromeAbertoDeveMostrarLeiloesNoTitulo()
        {
            #region Arrange
            #endregion

            #region Act
            _driver.Navigate().GoToUrl("http://localhost:5000");
            #endregion

            #region Assert
            Assert.Contains("Leilões", _driver.Title);
            #endregion
        }

        [Fact]
        public void DadoChromeAbertoDeveMostrarProximosLeiloesNaPagina()
        {
            #region Arrange
            #endregion

            #region Aact
            _driver.Navigate().GoToUrl("http://localhost:5000");
            #endregion

            #region Assert
            Assert.Contains("Próximos Leilões", _driver.PageSource);
            #endregion
        }

        [Fact]
        public void DadoChromeAbertoFormRegistroNaoDeveMostrarMensagensDeErro()
        {
            #region Arrange
            #endregion

            #region Act
            _driver.Navigate().GoToUrl("http://localhost:5000");
            #endregion

            #region Assert
            var form = _driver.FindElement(By.TagName("form"));
            var spans = form.FindElements(By.TagName("span"));

            foreach (var span in spans)
            {
                Assert.True(string.IsNullOrEmpty(span.Text));
            }
            #endregion
        }
    }
}
