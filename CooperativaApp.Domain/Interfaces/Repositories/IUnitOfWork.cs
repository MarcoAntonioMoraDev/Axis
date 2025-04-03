using System;
using System.Threading.Tasks;

namespace CooperativaApp.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        void BeginTransaction();
        Task BeginTransactionAsync();

        void Commit();
        Task CommitAsync();

        void Rollback();
        Task RollbackAsync();

        Task<int> SaveChangesAsync();

        ICooperativaRepository CooperativaRepository { get; }
        ICooperadoRepository CooperadoRepository { get; }
        IContatoFavoritoRepository ContatoFavoritoRepository { get; }
    }
}