using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class MenuLogadoPageObject
    {
        private IWebDriver _driver;
        private readonly By byLinkLogout;
        private readonly By byMenuMeuPerfilLink;

        public MenuLogadoPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byLinkLogout = By.Id("logout");
            byMenuMeuPerfilLink = By.Id("meu-perfil");
        }

        public void EfetuarLogout()
        {
            var elementlinkMeuPerfil = _driver.FindElement(byMenuMeuPerfilLink);
            var elementlinkLogout = _driver.FindElement(byLinkLogout);


            IAction actionLogout = new Actions(_driver).MoveToElement(elementlinkMeuPerfil) //Mover para o elemento pai
                                                       .MoveToElement(elementlinkLogout) //Mover para o elemento link de logout
                                                       .Click() //Clicar no link de logout
                                                       .Build();

            actionLogout.Perform(); // executa a ação (clicat no link de logout)
        }
    }
}
