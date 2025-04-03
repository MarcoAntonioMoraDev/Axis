using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApp.Infra.Data.SqlServer.Repositories
{
    public class CooperativaRepository : ICooperativaRepository
    {
        private readonly CooperativaDataContext _context;

        public CooperativaRepository(CooperativaDataContext context)
        {
            _context = context;
        }

        public async Task<Cooperativa?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Cooperativas
                                     .Include(c => c.Cooperados)
                                     .FirstOrDefaultAsync(c => c.CodigoCooperativaId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cooperativa com ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Cooperativa>> GetAllAsync()
        {
            try
            {
                return await _context.Cooperativas
                                     .Include(c => c.Cooperados)
                                     .Where(c => c.Ativo)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todas as cooperativas.", ex);
            }
        }

        public async Task<Cooperativa> AddAsync(Cooperativa cooperativa)
        {
            try
            {
                cooperativa.Ativo = true;
                await _context.Cooperativas.AddAsync(cooperativa);
                await _context.SaveChangesAsync();
                return cooperativa;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar cooperativa.", ex);
            }
        }

        public async Task UpdateAsync(Cooperativa cooperativa)
        {
            try
            {
                _context.Entry(cooperativa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cooperativa.", ex);
            }
        }

        public async Task<Cooperativa?> DeleteAsync(int id)
        {
            try
            {
                var cooperativa = await _context.Cooperativas.FindAsync(id);
                if (cooperativa == null) return null;

                cooperativa.Ativo = false;
                _context.Entry(cooperativa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return cooperativa;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar cooperativa.", ex);
            }
        }
    }
}