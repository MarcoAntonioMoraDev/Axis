using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Repositories
{
    public interface ICooperativaRepository
    {
        Task<Cooperativa?> GetByIdAsync(int id);
        Task<IEnumerable<Cooperativa>> GetAllAsync();
        Task<Cooperativa> AddAsync(Cooperativa cooperativa);
        Task UpdateAsync(Cooperativa cooperativa);
        Task<Cooperativa?> DeleteAsync(int id);
    }
}