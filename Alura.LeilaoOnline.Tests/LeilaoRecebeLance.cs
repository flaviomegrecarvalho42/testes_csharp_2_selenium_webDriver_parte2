using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    [Trait("Tipo", "Unidade")]
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            #region Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano");

            leilao.IniciarPregao();
            leilao.ReceberLance(fulano, 800);
            #endregion

            #region Act
            leilao.ReceberLance(fulano, 1000);
            #endregion

            #region Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
            #endregion
        }

        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] lances)
        {
            #region Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano");
            var maria = new Interessada("Maria");

            leilao.IniciarPregao();

            for (int i = 0; i < lances.Length; i++)
            {
                var valor = lances[i];

                if ((i % 2) == 0)
                {
                    leilao.ReceberLance(fulano, valor);
                }
                else
                {
                    leilao.ReceberLance(maria, valor);
                }
            }

            leilao.TerminarPregao();
            #endregion

            #region Act
            leilao.ReceberLance(fulano, 1000);
            #endregion

            #region Assert
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
            #endregion
        }
    }
}
