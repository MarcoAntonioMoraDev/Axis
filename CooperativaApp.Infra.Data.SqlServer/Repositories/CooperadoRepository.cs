using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApp.Infra.Data.SqlServer.Repositories
{
    public class CooperadoRepository : ICooperadoRepository
    {
        private readonly CooperativaDataContext _context;

        public CooperadoRepository(CooperativaDataContext context) => _context = context;

        public async Task<Cooperado?> GetByIdAsync(int id) => await _context.Cooperados.FindAsync(id);

        public async Task<IEnumerable<Cooperado>> GetByNomeAsync(string nome) =>
            await _context.Cooperados.Where(c => c.Nome.Contains(nome)).ToListAsync();

        public async Task<IEnumerable<Cooperado>> GetByContaAsync(string conta) =>
            await _context.Cooperados.Where(c => c.Conta.Contains(conta)).ToListAsync();

        public async Task<IEnumerable<Cooperado>> GetAllAsync() => await _context.Cooperados.ToListAsync();

        public async Task<Cooperado> AddAsync(Cooperado cooperado)
        {
            try
            {
                cooperado.Ativo = true;
                await _context.Cooperados.AddAsync(cooperado);
                await _context.SaveChangesAsync();
                return cooperado;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar cooperado.", ex);
            }
        }

        public async Task UpdateAsync(Cooperado cooperado)
        {
            try
            {
                _context.Entry(cooperado).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cooperado.", ex);
            }
        }
    }
}