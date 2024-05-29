

using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizado_QuandoAplicadoDesconto()
        {
            //ARRANGE
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 02, 01), new DateTime(2024, 02, 05));
            double precoOriginal = 100.00;
            double desconto = 20.00;
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            // ACT
            oferta.Desconto = desconto;

            //ASSERT
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Fact]
        public void RetornaPrecoCom70PorcentoDesconto_QuandoDescontoMaiorQuePreco()
        {
            //ARRANGE
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 02, 01), new DateTime(2024, 02, 05));
            double precoOriginal = 100.00;
            double desconto = 120.00;
            double precoComDesconto = 30;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            // ACT
            oferta.Desconto = desconto;

            //ASSERT
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }
    }
}
