using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Selenium.Helpers
{
    public class DropDownMaterialize
    {
        private IWebDriver _driver;
        private IWebElement dropDownWrapper;

        public DropDownMaterialize(IWebDriver driver, By locatorDroDownWrapper)
        {
            _driver = driver;
            dropDownWrapper = _driver.FindElement(locatorDroDownWrapper);
            Options = dropDownWrapper.FindElements(By.CssSelector("li>span"));
        }

        public IEnumerable<IWebElement> Options { get; }

        /// <summary>
        /// Desmarcar todas as opções
        /// </summary>
        public void DeselectAll()
        {
            OpenDropDownWrapper();

            Options.ToList()
                   .ForEach(o =>
                   {
                       o.Click();
                   });

            LoseFocus();
        }

        /// <summary>
        /// Marcar a opção da categoria, quando ele estiver na lista de categorias
        /// </summary>
        /// <param name="option"></param>
        public void SelectByText(string option)
        {
            OpenDropDownWrapper();

            Options.Where(o => o.Text.Contains(option))
                   .ToList()
                   .ForEach(o =>
                   {
                       o.Click();
                   });

            LoseFocus();
        }

        /// <summary>
        /// Clicar no dropdown, listando as opções
        /// </summary>
        private void OpenDropDownWrapper()
        {
            dropDownWrapper.Click();
        }

        /// <summary>
        /// Retirar o foco do dropdown de categorias
        /// </summary>
        private void LoseFocus()
        {
            dropDownWrapper.FindElement(By.TagName("li")).SendKeys(Keys.Tab);
        }
    }
}
