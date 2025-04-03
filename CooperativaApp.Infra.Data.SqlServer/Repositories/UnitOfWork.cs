using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace CooperativaApp.Infra.Data.SqlServer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly CooperativaDataContext _dataContext;
        private IDbContextTransaction? _transaction;
        private readonly ICooperativaRepository _cooperativaRepository;
        private readonly ICooperadoRepository _cooperadoRepository;
        private readonly IContatoFavoritoRepository _contatoFavoritoRepository;


        public UnitOfWork(CooperativaDataContext dataContext,
                          ICooperativaRepository cooperativaRepository,
                          ICooperadoRepository cooperadoRepository,
                          IContatoFavoritoRepository contatoFavoritoRepository)
        {
            _dataContext = dataContext;
            _cooperativaRepository = cooperativaRepository;
            _cooperadoRepository = cooperadoRepository;
            _contatoFavoritoRepository = contatoFavoritoRepository;
        }

        public void BeginTransaction()
        {
            try
            {
                _transaction = _dataContext.Database.BeginTransaction();
            }
            catch (Exception ex)
            { 
                throw new Exception("Erro ao iniciar transação.", ex);
            }
        }

        public async Task BeginTransactionAsync()
        {
            try
            {
                _transaction = await _dataContext.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao iniciar transação assíncrona.", ex);
            }
        }

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao confirmar transação.", ex);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction?.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao confirmar transação assíncrona.", ex);
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao reverter transação.", ex);
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction?.RollbackAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao reverter transação assíncrona.", ex);
            }
        }


        public async Task<int> SaveChangesAsync() => await _dataContext.SaveChangesAsync();

        public ICooperativaRepository CooperativaRepository => _cooperativaRepository;
        public ICooperadoRepository CooperadoRepository => _cooperadoRepository;
        public IContatoFavoritoRepository ContatoFavoritoRepository => _contatoFavoritoRepository;

        public void Dispose() => _dataContext.Dispose();
        public async ValueTask DisposeAsync() => await _dataContext.DisposeAsync();
    }
}