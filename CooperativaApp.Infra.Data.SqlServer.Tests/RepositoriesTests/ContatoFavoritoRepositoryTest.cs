using CooperativaApp.Domain.Entities;
using Bogus;
using Bogus.Extensions.Brazil;
using CooperativaApp.Infra.Data.SqlServer.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace CooperativaApp.Infra.Data.SqlServer.Tests.RepositoriesTests
{
    public class ContatoFavoritoRepositoryTest : IDisposable
    {
        private readonly Faker<ContatoFavorito> _fakerContatoFavorito;
        private readonly CooperativaDataContext _context;
        private readonly ContatoFavoritoRepository _contatoFavoritoRepository;

        public ContatoFavoritoRepositoryTest()
        {
            _fakerContatoFavorito = new Faker<ContatoFavorito>("pt_BR")
                .RuleFor(c => c.Nome, f => f.Person.FullName)
                .RuleFor(c => c.TipoChavePix, f => f.PickRandom<TipoChavePix>())
                .RuleFor(c => c.ChavePix, (f, c) =>
                {
                    switch (c.TipoChavePix)
                    {
                        case TipoChavePix.CPF:
                            return f.Person.Cpf();
                        case TipoChavePix.CNPJ:
                            return f.Company.Cnpj();
                        case TipoChavePix.Email:
                            return f.Internet.Email();
                        case TipoChavePix.Telefone:
                            return f.Phone.PhoneNumber();
                        case TipoChavePix.Aleatoria:
                            return f.Random.AlphaNumeric(8);
                        default:
                            return "";
                    }
                })
                .RuleFor(c => c.CodigoCooperadoId, f => f.Random.Int(1, 30));


            var options = new DbContextOptionsBuilder<CooperativaDataContext>()
                .UseInMemoryDatabase(databaseName: "ContatoFavoritoAppDBTests")
                .Options;

            _context = new CooperativaDataContext(options);
            _context.Database.EnsureCreated();

            // Preenche o banco com alguns Cooperados para criar referências válidas
            _context.Cooperados.AddRange(Enumerable.Range(1, 30).Select(i => new Cooperado
            {
                CodigoCooperadoId = i,
                Nome = $"Cooperado {i}",
                Ativo = true,
                CodigoCooperativaId = 1,
                Banco = "01",
                Agencia = "1234",
                Conta = "567890",
                DigitoVerificador = "1"
            }));
            _context.SaveChanges();

            _contatoFavoritoRepository = new ContatoFavoritoRepository(_context);
        }

        [Fact(DisplayName = "Adicionar Contato Favorito no repositório")]
        public async Task AddAsync_ShouldAddContatoFavorito()
        {
            var contatoFavorito = _fakerContatoFavorito.Generate();

            await _contatoFavoritoRepository.AddAsync(contatoFavorito);

            Assert.True(_context.ContatosFavoritos.Any(c => c.CodigoContatoFavoritoId == contatoFavorito.CodigoContatoFavoritoId));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}