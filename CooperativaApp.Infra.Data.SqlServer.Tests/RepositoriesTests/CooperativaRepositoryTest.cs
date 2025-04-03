using CooperativaApp.Domain.Entities;
using Bogus;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using CooperativaApp.Infra.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CooperativaApp.Infra.Data.SqlServer.Tests.RepositoriesTests
{
    public class CooperativaRepositoryTest : IDisposable
    {
        private readonly Faker<Cooperativa> _fakerCooperativa;
        private readonly CooperativaDataContext _context;
        private readonly CooperativaRepository _cooperativaRepository;

        public CooperativaRepositoryTest()
        {
            _fakerCooperativa = new Faker<Cooperativa>("pt_BR")
                .RuleFor(c => c.CodigoCooperativaId, f => f.Random.Int())
                .RuleFor(c => c.Nome, f => f.Company.CompanyName())
                .RuleFor(c => c.Ativo, f => true);

            var options = new DbContextOptionsBuilder<CooperativaDataContext>()
                .UseInMemoryDatabase(databaseName: "CooperativaAppDBTests")
                .Options;

            _context = new CooperativaDataContext(options);
            _context.Database.EnsureCreated();

            _cooperativaRepository = new CooperativaRepository(_context);
        }

        [Fact(DisplayName = "Adicionar Cooperativa no repositório")]
        public async Task AddAsync_ShouldAddCooperativa()
        {
            var cooperativa = _fakerCooperativa.Generate();

            await _cooperativaRepository.AddAsync(cooperativa);

            Assert.True(_context.Cooperativas.Any(c => c.CodigoCooperativaId == cooperativa.CodigoCooperativaId));
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}