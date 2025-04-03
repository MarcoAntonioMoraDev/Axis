using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Domain.Interfaces.Services;

namespace CooperativaApp.Domain.Services
{
    public class CooperadoDomainService : ICooperadoDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CooperadoDomainService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Cooperado?> GetByIdAsync(int id) => await _unitOfWork.CooperadoRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Cooperado>> GetAllAsync() => await _unitOfWork.CooperadoRepository.GetAllAsync();

        public async Task<Cooperado?> AddAsync(Cooperado cooperado)
        {
            try
            {
                cooperado.Ativo = true;
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.CooperadoRepository.AddAsync(cooperado);
                await _unitOfWork.CommitAsync();
                return cooperado;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao adicionar cooperado.", ex);
            }
        }

        public async Task<Cooperado?> UpdateAsync(Cooperado cooperado)
        {
            try
            {
                var existingCooperado = await _unitOfWork.CooperadoRepository.GetByIdAsync(cooperado.CodigoCooperadoId);
                if (existingCooperado == null) return null;

                existingCooperado.Nome = cooperado.Nome;
                existingCooperado.Ativo = cooperado.Ativo;
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.CooperadoRepository.UpdateAsync(existingCooperado);
                await _unitOfWork.CommitAsync();
                return existingCooperado;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao atualizar cooperado.", ex);
            }
        }

        public async Task<Cooperado?> DeleteAsync(int id)
        {
            try
            {
                var cooperado = await _unitOfWork.CooperadoRepository.GetByIdAsync(id);
                if (cooperado == null) return null;

                cooperado.Ativo = false;
                await _unitOfWork.BeginTransactionAsync();
                _unitOfWork.CooperadoRepository.UpdateAsync(cooperado);
                await _unitOfWork.CommitAsync();
                return cooperado;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao deletar cooperado.", ex);
            }
        }
    }
}