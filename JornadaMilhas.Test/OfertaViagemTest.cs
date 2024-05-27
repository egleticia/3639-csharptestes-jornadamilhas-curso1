using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemTest
    {
        [Fact]
        public void TestandoOfertaValida()
        {
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024,02,01), new DateTime(2024,02,05));
            double preco = 100.00;
            var validacao = true;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            Assert.Equal(validacao, oferta.EhValido);
        }
    }
}