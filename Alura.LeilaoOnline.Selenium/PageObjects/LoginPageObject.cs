using OpenQA.Selenium;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class LoginPageObject
    {
        private IWebDriver _driver;
        private readonly By byInputLogin;
        private readonly By byInputSenha;
        private readonly By byButtonLogin;

        public LoginPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byInputLogin = By.Id("Login");
            byInputSenha = By.Id("Password");
            byButtonLogin = By.Id("btnLogin");
        }

        public LoginPageObject AbrirPagina()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Autenticacao/Login");
            return this;
        }

        public LoginPageObject InformarEmail(string login)
        {
            _driver.FindElement(byInputLogin).SendKeys(login);
            return this;
        }

        public LoginPageObject InformarSenha(string senha)
        {
            _driver.FindElement(byInputSenha).SendKeys(senha);
            return this;
        }

        public LoginPageObject EfetuarLogin()
        {
            _driver.FindElement(byButtonLogin).Submit();
            return this;
        }
    }
}
