using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Services
{
    public interface ICooperativaDomainService
    {
        Task<Cooperativa?> GetByIdAsync(int id);
        Task<IEnumerable<Cooperativa>> GetAllAsync();
        Task<Cooperativa?> AddAsync(Cooperativa cooperativa);
        Task<Cooperativa?> UpdateAsync(Cooperativa cooperativa);
        Task<Cooperativa?> DeleteAsync(int id);
    }
}