using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Interfaces.Services
{
    public interface ICooperadoService
    {
        Task<CooperadoDTOResponse?> GetByIdAsync(int id);
        Task<IEnumerable<CooperadoDTOResponse>> GetByNomeAsync(string nome);
        Task<IEnumerable<CooperadoDTOResponse>> GetByContaAsync(string conta);
        Task<IEnumerable<CooperadoDTOResponse>> GetAllAsync();
        Task<CooperadoDTOResponse> AddAsync(CooperadoDTORequest cooperadoDTORequest);
        Task UpdateAsync(CooperadoDTOResponse cooperadoDto);
        Task<Cooperado?> DeleteAsync(int id);
    }
}