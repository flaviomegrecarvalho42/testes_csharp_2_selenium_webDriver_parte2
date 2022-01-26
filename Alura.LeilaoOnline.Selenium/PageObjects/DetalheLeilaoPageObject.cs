using OpenQA.Selenium;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class DetalheLeilaoPageObject
    {
        private IWebDriver _driver;
        private readonly By byInputValorLance;
        private readonly By byButtonSalvar;
        private readonly By byLableLanceAtual;

        public DetalheLeilaoPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byInputValorLance = By.Id("Valor");
            byButtonSalvar = By.Id("btnDarLance");
            byLableLanceAtual = By.Id("lanceAtual");
        }

        public double LanceAtual
        {
            get
            {
                var valorLableLanceAtual = _driver.FindElement(byLableLanceAtual).Text.Replace("R$ ", "").Replace(",", ".");
                var valorLanceAtual = double.Parse(valorLableLanceAtual, System.Globalization.NumberStyles.Currency);
                return valorLanceAtual;
            }
        }

        public void AbrirPagina(int idLeilao)
        {
            _driver.Navigate().GoToUrl($"http://localhost:5000/Home/Detalhes/{idLeilao}");
        }

        public void DarLance(double valorLance)
        {
            _driver.FindElement(byInputValorLance).Clear();
            _driver.FindElement(byInputValorLance).SendKeys(valorLance.ToString());
            _driver.FindElement(byButtonSalvar).Click();
        }
    }
}
