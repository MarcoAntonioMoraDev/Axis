using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddAsync(Usuario usuario);

        Task<bool> AnyAsync(string email);

        Task<Usuario?> FindAsync(string email, string senha);
    }
}
