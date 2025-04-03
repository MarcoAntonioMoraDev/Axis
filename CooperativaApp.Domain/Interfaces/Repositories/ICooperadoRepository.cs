using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Repositories
{
    public interface ICooperadoRepository
    {
        Task<Cooperado?> GetByIdAsync(int id);
        Task<IEnumerable<Cooperado>> GetByNomeAsync(string nome);
        Task<IEnumerable<Cooperado>> GetByContaAsync(string conta);
        Task<IEnumerable<Cooperado>> GetAllAsync();
        Task<Cooperado> AddAsync(Cooperado cooperado);
        Task UpdateAsync(Cooperado cooperado);
    }
}