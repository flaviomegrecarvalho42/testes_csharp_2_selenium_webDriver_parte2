using System.ComponentModel.DataAnnotations;

namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Lance(double valor)
        {
            ValidarValorDoLance(valor);
            Valor = valor;
        }

        public int Id { get; set; }
        public double Valor { get; private set; }

        [Required]
        public Interessada Cliente { get; set; }

        [Required]
        public Leilao Leilao { get; set; }

        public Lance(Interessada cliente, double valor)
        {
            ValidarValorDoLance(valor);
            Cliente = cliente;
            Valor = valor;
        }

        private void ValidarValorDoLance(double valor)
        {
            if (valor < 0)
            {
                throw new System.ArgumentException("Valor do lance deve ser igual ou maior que zero.");
            }
        }
    }
}
