using CooperativaApp.Application.DTOs;

namespace CooperativaApp.Application.Interfaces.Services
{
    public interface IContatoFavoritoService
    {
        Task<ContatoFavoritoDTOResponse?> GetByIdAsync(int id);
        Task<IEnumerable<ContatoFavoritoDTOResponse>> GetAllAsync();
        Task<ContatoFavoritoDTOResponse> AddAsync(ContatoFavoritoDTORequest contatoFavoritoDTORequest);
        Task UpdateAsync(ContatoFavoritoDTORequest contatoFavoritoDto);
        Task<ContatoFavorito?> DeleteAsync(int id);
    }
}
