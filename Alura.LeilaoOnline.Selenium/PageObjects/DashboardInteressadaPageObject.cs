using OpenQA.Selenium;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class DashboardInteressadaPageObject
    {
        private IWebDriver _driver;

        public DashboardInteressadaPageObject(IWebDriver driver)
        {
            this._driver = driver;
            Filtro = new FiltroLeiloesPageObject(_driver);
            Menu = new MenuLogadoPageObject(_driver);
        }

        public FiltroLeiloesPageObject Filtro { get; }
        public MenuLogadoPageObject Menu { get; }
    }
}
