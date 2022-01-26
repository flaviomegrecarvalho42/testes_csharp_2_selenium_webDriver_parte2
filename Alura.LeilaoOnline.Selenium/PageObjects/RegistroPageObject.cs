using OpenQA.Selenium;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class RegistroPageObject
    {
        private IWebDriver _driver;
        private readonly By byFormRegistro;
        private readonly By byInputNome;
        private readonly By byInputEmail;
        private readonly By byInputSenha;
        private readonly By byInputConfirmSenha;
        private readonly By byButtonRegistro;
        private readonly By bySpanErroEmail;
        private readonly By bySpanErroNome;

        public string MensagemErrorEmail => _driver.FindElement(bySpanErroEmail).Text;

        public string MensagemErrorNome => _driver.FindElement(bySpanErroNome).Text;

        public RegistroPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byFormRegistro = By.TagName("form");
            byInputNome = By.Id("Nome");
            byInputEmail = By.Id("Email");
            byInputSenha = By.Id("Password");
            byInputConfirmSenha = By.Id("ConfirmPassword");
            byButtonRegistro = By.Id("btnRegistro");
            bySpanErroEmail = By.CssSelector("span.msg-erro[data-valmsg-for=Email]");
            bySpanErroNome = By.CssSelector("span.msg-erro[data-valmsg-for=Nome]");
        }

        public void AbrirPagina()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000");
        }

        public void SubmeterFormulario()
        {
            _driver.FindElement(byButtonRegistro).Click();
        }

        public void PreencherFormulario(string nome,
                                        string email,
                                        string senha,
                                        string confirmSenha)
        {
            _driver.FindElement(byInputNome).SendKeys(nome);
            _driver.FindElement(byInputEmail).SendKeys(email);
            _driver.FindElement(byInputSenha).SendKeys(senha);
            _driver.FindElement(byInputConfirmSenha).SendKeys(confirmSenha);
        }
    }
}
