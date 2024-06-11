
using Bogus;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class GerenciadorDeOfertasRecuperaMaiorDesconto
    {
        [Fact]
        public void RetornarOfertaNulaQuandoListaVazia()
        {
            //ARRANGE
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

            //ACT
            var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

            //ASSET
            Assert.Null(oferta);
        }

        [Fact]
        public void RetornarOfertaofertaEspecificaQuandoDestinoSaoPauloEDesconto40()
        {
            //ARRANGE
            var fakerPeriodo = new Faker<Periodo>().CustomInstantiator(f =>
            {
                DateTime dataInicio = f.Date.Soon();
                return new Periodo(dataInicio, dataInicio.AddDays(30));
            });

            var rota = new Rota("Curitiba", "São Paulo");
            var fakeroferta = new Faker<OfertaViagem>().CustomInstantiator(f => new OfertaViagem(
                rota,
                fakerPeriodo.Generate(),
                100 * f.Random.Int(1, 100)))
                .RuleFor(o => o.Desconto, f => 40)
                .RuleFor(o => o.Ativa, f => true);

            var ofertaEscolhida = new OfertaViagem(rota, fakerPeriodo.Generate(), 80)
            {
                Desconto = 40,
                Ativa = true
            };

            var ofertaInativa = new OfertaViagem(rota, fakerPeriodo.Generate(), 70)
            {
                Desconto = 40,
                Ativa = false
            };

            var lista = fakeroferta.Generate(200);
            lista.Add(ofertaEscolhida);
            lista.Add(ofertaInativa);
            var gerenciador = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
            var precoEsperado = 40;

            //ACT
            var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

            //ASSET
            Assert.NotNull(oferta);
            Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
        }
    }
}
