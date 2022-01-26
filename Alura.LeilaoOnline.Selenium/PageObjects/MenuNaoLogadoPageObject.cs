using OpenQA.Selenium;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class MenuNaoLogadoPageObject
    {
        private IWebDriver _driver;
        private By byMenuMobile;

        public MenuNaoLogadoPageObject(IWebDriver driver)
        {
            this._driver = driver;
            byMenuMobile = By.ClassName("sidenav-trigger"); // Ou By.CssSelector(".sidenav-trigger")
        }

        public bool MenuMobileIsVisible
        {
            get
            {
                var element = _driver.FindElement(byMenuMobile);
                return element.Displayed;
            }
        }
    }
}