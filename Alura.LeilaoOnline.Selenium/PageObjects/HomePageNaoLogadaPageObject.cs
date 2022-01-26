using System;
using OpenQA.Selenium;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class HomePageNaoLogadaPageObject
    {
        private IWebDriver _driver;

        public HomePageNaoLogadaPageObject(IWebDriver driver)
        {
            this._driver = driver;
            Menu = new MenuNaoLogadoPageObject(_driver);
        }

        public MenuNaoLogadoPageObject Menu { get; internal set; }

        public void AbrirPagina()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/");
        }
    }
}
