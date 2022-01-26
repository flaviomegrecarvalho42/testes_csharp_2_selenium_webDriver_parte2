using Alura.LeilaoOnline.Selenium.Helpers;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class FiltroLeiloesPageObject
    {
        private IWebDriver _driver;
        private readonly By byDropDownCategorias;
        private readonly By byInputTermoDePesquisa;
        private readonly By byInputEmAndamento;
        private readonly By byButtonPesquisar;

        public FiltroLeiloesPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byDropDownCategorias = By.ClassName("select-wrapper");
            byInputTermoDePesquisa = By.Id("termo");
            byInputEmAndamento = By.ClassName("switch");
            byButtonPesquisar = By.CssSelector("form>button.btn");
        }

        public void PesquisarLeiloes(List<string> categorias, string termoDePesquisa, bool emAndamento)
        {
            //Capturar o elemento select do dropdown do materialize
            var dropDownWrapper = new DropDownMaterialize(_driver, byDropDownCategorias);

            //Desmarcar todas as options do select
            dropDownWrapper.DeselectAll();

            //Passar por cada categoria e verificar se ela consta em alguma das options do select
            //Caso a opção do select corresponda, deverá marcá-la com o click
            categorias.ForEach(c =>
            {
                dropDownWrapper.SelectByText(c);
            });

            _driver.FindElement(byInputTermoDePesquisa).SendKeys(termoDePesquisa);

            if (emAndamento)
            {
                _driver.FindElement(byInputEmAndamento).Click();
            }

            _driver.FindElement(byButtonPesquisar).Click();
        }
    }
}
