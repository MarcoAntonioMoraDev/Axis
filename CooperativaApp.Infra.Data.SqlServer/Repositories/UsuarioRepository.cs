using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApp.Infra.Data.SqlServer.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CooperativaDataContext _context;

        public UsuarioRepository(CooperativaDataContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> AnyAsync(string email)
        {
            return await _context
                            .Set<Usuario>()
                            .Where(u => u.Email.Equals(email))
                            .AnyAsync();
        }

        public async Task<Usuario?> FindAsync(string email, string senha)
        {
            return await _context
                            .Set<Usuario>()
                            .FirstOrDefaultAsync(
                                    u => u.Email.Equals(email)
                                 && u.Senha.Equals(senha));
        }
    }
}
