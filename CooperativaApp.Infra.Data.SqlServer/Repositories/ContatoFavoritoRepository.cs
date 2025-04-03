using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApp.Infra.Data.SqlServer.Repositories
{
    public class ContatoFavoritoRepository : IContatoFavoritoRepository
    {
        private readonly CooperativaDataContext _context;

        public ContatoFavoritoRepository(CooperativaDataContext context)
        {
            _context = context;
        }

        public async Task<ContatoFavorito?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ContatosFavoritos
                                     .Include(c => c.Cooperado)
                                     .FirstOrDefaultAsync(c => c.CodigoCooperadoId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter contato favorito com ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ContatoFavorito>> GetAllAsync()
        {
            try
            {
                return await _context.ContatosFavoritos
                                     .Include(c => c.Cooperado)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os contatos favoritos.", ex);
            }
        }

        public async Task<ContatoFavorito> AddAsync(ContatoFavorito contatoFavorito)
        {
            try
            {
                await _context.ContatosFavoritos.AddAsync(contatoFavorito);
                await _context.SaveChangesAsync();
                return contatoFavorito;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar contato favorito.", ex);
            }
        }

        public async Task UpdateAsync(ContatoFavorito contatoFavorito)
        {
            try
            {
                _context.Entry(contatoFavorito).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar contato favorito.", ex);
            }
        }

        public async Task<ContatoFavorito?> DeleteAsync(int id)
        {
            try
            {
                var contatoFavorito = await _context.ContatosFavoritos.FindAsync(id);
                if (contatoFavorito == null) return null;

                _context.ContatosFavoritos.Remove(contatoFavorito);
                await _context.SaveChangesAsync();
                return contatoFavorito;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar contato favorito.", ex);
            }
        }
    }
}
