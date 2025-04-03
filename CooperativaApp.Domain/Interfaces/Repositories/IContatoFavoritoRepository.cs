using CooperativaApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CooperativaApp.Domain.Interfaces.Repositories
{
    public interface IContatoFavoritoRepository
    {
        Task<ContatoFavorito?> GetByIdAsync(int id);
        Task<IEnumerable<ContatoFavorito>> GetAllAsync();
        Task<ContatoFavorito> AddAsync(ContatoFavorito contatoFavorito);
        Task UpdateAsync(ContatoFavorito contatoFavorito);
        Task<ContatoFavorito?> DeleteAsync(int id);
    }
}