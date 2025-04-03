using CooperativaApp.Domain.Entities;
using Bogus;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using CooperativaApp.Infra.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CooperativaApp.Infra.Data.SqlServer.Tests.RepositoriesTests
{
    public class CooperadoRepositoryTest : IDisposable
    {
        private readonly Faker<Cooperado> _fakerCooperado;
        private readonly CooperativaDataContext _context;
        private readonly CooperadoRepository _cooperadoRepository;

        public CooperadoRepositoryTest()
        {
            _fakerCooperado = new Faker<Cooperado>("pt_BR")
                .RuleFor(c => c.Nome, f => f.Person.FullName)
                .RuleFor(c => c.Banco, f => f.Finance.AccountName())
                .RuleFor(c => c.Agencia, f => f.Finance.Account())
                .RuleFor(c => c.Conta, f => f.Finance.Account())
                .RuleFor(c => c.DigitoVerificador, f => f.Finance.CreditCardCvv())
                .RuleFor(c => c.Ativo, f => true)
                .RuleFor(c => c.CodigoCooperativaId, f => f.Random.Int(1, 30));


            var options = new DbContextOptionsBuilder<CooperativaDataContext>()
                .UseInMemoryDatabase(databaseName: "CooperadoAppDBTests")
                .Options;

            _context = new CooperativaDataContext(options);
            _context.Database.EnsureCreated();

            // Cria algumas cooperativas para que o teste tenha referências válidas.
            _context.Cooperativas.AddRange(Enumerable.Range(1, 10).Select(i => new Cooperativa { CodigoCooperativaId = i, Nome = $"Cooperativa {i}", Ativo = true }));
            _context.SaveChanges();


            _cooperadoRepository = new CooperadoRepository(_context);
        }

        [Fact(DisplayName = "Adicionar Cooperado no repositório")]
        public async Task AddAsync_ShouldAddCooperado()
        {
            var cooperado = _fakerCooperado.Generate();

            await _cooperadoRepository.AddAsync(cooperado);

            Assert.True(_context.Cooperados.Any(c => c.CodigoCooperadoId == cooperado.CodigoCooperadoId));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}