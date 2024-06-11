using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-20", "2024-01-25", 0, false)]
        [InlineData(null, "DestinoTeste", "2024-01-20", "2024-01-25", -300.00, false)]
        [InlineData(null, null, "2024-01-20", "2024-01-25", 100.00, true)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-01-20", "2024-01-25", 100.00, true)]
        public void RetornaEhValido_DadosEntrada(string origem, string destino, string dataIda, 
            string dataVolta, double preco, bool validacao)
        {
            //ARRANGE
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //ACT
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //ASSERT
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemErroRotaOuPeriodo_QuandoRotaNula()
        {
            //ARRANGE
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 02, 01), new DateTime(2024, 02, 05));
            double preco = -100.00;

            //ACT
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //ASSERT
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void RetornaMensagemErroPrecoInvalido_QuandoPrecoNegativo(double preco)
        {
            //ARRANGE
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 02, 01), new DateTime(2024, 02, 05));

            //ACT
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //ASSERT
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoInvalidos()
        {
            // ARRANGE
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 6, 1), new DateTime(2024, 5, 10));
            double preco = -100;
            int quantidadeEsperada = 3;

            // ACT
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);


            // ASSERT
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }


    }
}