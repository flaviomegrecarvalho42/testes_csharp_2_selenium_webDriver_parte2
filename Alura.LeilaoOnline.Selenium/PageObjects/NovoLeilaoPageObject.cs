using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class NovoLeilaoPageObject
    {
        private IWebDriver _driver;
        private readonly By byInputTitulo;
        private readonly By byInputDescricao;
        private readonly By byInputCategoria;
        private readonly By byInputValorInicial;
        private readonly By byInputImagem;
        private readonly By byInputDataInicioPregao;
        private readonly By byInputDataTerminoPregao;
        private readonly By byButtonSalvar;

        public NovoLeilaoPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byInputTitulo = By.Id("Titulo");
            byInputDescricao = By.Id("Descricao");
            byInputCategoria = By.Id("Categoria");
            byInputValorInicial = By.Id("ValorInicial");
            byInputImagem = By.Id("ArquivoImagem");
            byInputDataInicioPregao = By.Id("InicioPregao");
            byInputDataTerminoPregao = By.Id("TerminoPregao");
            byButtonSalvar = By.CssSelector("button[type=submit]");
        }

        public IEnumerable<string> Categorias
        {
            get
            {
                var elementDropDownCategoria = new SelectElement(_driver.FindElement(byInputCategoria));

                return elementDropDownCategoria.Options
                                               .Where(o => o.Enabled)
                                               .Select(o => o.Text);
            }
        }

        public void AbrirPagina()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Leiloes/Novo");
        }

        public void PreencherFormulario(string titulo,
                                        string descricao,
                                        string categoria,
                                        double valorInicial,
                                        string imagem,
                                        DateTime dataInicioPregao,
                                        DateTime dataTerminoPregao)
        {
            _driver.FindElement(byInputTitulo).SendKeys(titulo);
            _driver.FindElement(byInputDescricao).SendKeys(descricao);
            _driver.FindElement(byInputCategoria).SendKeys(categoria);
            _driver.FindElement(byInputValorInicial).SendKeys(valorInicial.ToString());
            _driver.FindElement(byInputImagem).SendKeys(imagem);
            _driver.FindElement(byInputDataInicioPregao).SendKeys(dataInicioPregao.ToString("dd/MM/yyyy"));
            _driver.FindElement(byInputDataTerminoPregao).SendKeys(dataTerminoPregao.ToString("dd/MM/yyyy"));
        }

        public void SubmeterFormulario()
        {
            _driver.FindElement(byButtonSalvar).Submit();
        }
    }
}
