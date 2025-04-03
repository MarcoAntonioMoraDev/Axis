using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Services
{
    public interface ICooperadoDomainService
    {
        Task<Cooperado?> GetByIdAsync(int id);
        Task<IEnumerable<Cooperado>> GetAllAsync();
        Task<Cooperado?> AddAsync(Cooperado cooperado);
        Task<Cooperado?> UpdateAsync(Cooperado cooperado);
        Task<Cooperado?> DeleteAsync(int id);
    }
}