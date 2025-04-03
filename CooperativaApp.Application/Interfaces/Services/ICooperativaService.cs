using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Interfaces.Services
{
    public interface ICooperativaService
    {
        Task<CooperativaDTOResponse?> GetByIdAsync(int id);
        Task<IEnumerable<CooperativaDTOResponse>> GetAllAsync();
        Task<CooperativaDTOResponse> AddAsync(CooperativaDTORequest cooperativaDTORequest);
        Task<CooperativaDTOResponse?> UpdateAsync(int id, CooperativaDTORequest cooperativaDTORequest);
        Task<Cooperativa?> DeleteAsync(int id);
    }
}