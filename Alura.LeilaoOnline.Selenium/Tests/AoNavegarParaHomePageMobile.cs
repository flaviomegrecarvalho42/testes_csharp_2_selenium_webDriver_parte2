using Alura.LeilaoOnline.Selenium.Helpers;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoNavegarParaHomePageMobile : IDisposable
    {
        private ChromeDriver _driver;

        public AoNavegarParaHomePageMobile()
        {
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Fact]
        public void DadaLarguraDaPagina992DeveMostarMenuMobile()
        {
            #region Arrange
            ConfigurarHomePageMobile(992);
            HomePageNaoLogadaPageObject homePageNaoLogadaPageObject = new HomePageNaoLogadaPageObject(_driver);
            #endregion

            #region Act
            homePageNaoLogadaPageObject.AbrirPagina();
            Thread.Sleep(3000);
            #endregion

            #region Assert
            Assert.True(homePageNaoLogadaPageObject.Menu.MenuMobileIsVisible);
            #endregion
        }

        [Fact]
        public void DadaLarguraDaPagina993NaoDeveMostarMenuMobile()
        {
            #region Arrange
            ConfigurarHomePageMobile(993);
            HomePageNaoLogadaPageObject homePageNaoLogadaPageObject = new HomePageNaoLogadaPageObject(_driver);
            #endregion

            #region Act
            homePageNaoLogadaPageObject.AbrirPagina();
            Thread.Sleep(3000);
            #endregion

            #region Assert
            Assert.False(homePageNaoLogadaPageObject.Menu.MenuMobileIsVisible);
            #endregion
        }

        private void ConfigurarHomePageMobile(int tamanhoPagina)
        {
            ChromeMobileEmulationDeviceSettings deviceSettings = new ChromeMobileEmulationDeviceSettings
            {
                Width = tamanhoPagina,
                Height = 800,
                UserAgent = "Customizada"
            };

            ChromeOptions options = new ChromeOptions();
            options.EnableMobileEmulation(deviceSettings);

            _driver = new ChromeDriver(TestHelper.PathDoExecutavel, options);
        }
    }
}
