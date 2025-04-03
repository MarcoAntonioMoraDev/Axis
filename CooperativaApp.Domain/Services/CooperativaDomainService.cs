using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Domain.Interfaces.Services;

namespace CooperativaApp.Domain.Services
{
    public class CooperativaDomainService : ICooperativaDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CooperativaDomainService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Cooperativa?> GetByIdAsync(int id) => await _unitOfWork.CooperativaRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Cooperativa>> GetAllAsync() => await _unitOfWork.CooperativaRepository.GetAllAsync();

        public async Task<Cooperativa?> AddAsync(Cooperativa cooperativa)
        {
            try
            {
                cooperativa.Ativo = true;
                await _unitOfWork.CooperativaRepository.AddAsync(cooperativa);
                await _unitOfWork.CommitAsync();
                return cooperativa;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao adicionar cooperativa.", ex);
            }
        }

        public async Task<Cooperativa?> UpdateAsync(Cooperativa cooperativa)
        {
            try
            {
                var existingCooperativa = await _unitOfWork.CooperativaRepository.GetByIdAsync(cooperativa.CodigoCooperativaId);
                if (existingCooperativa == null) return null;

                existingCooperativa.Nome = cooperativa.Nome;
                existingCooperativa.Ativo = cooperativa.Ativo;
                _unitOfWork.CooperativaRepository.UpdateAsync(existingCooperativa);
                await _unitOfWork.CommitAsync();
                return existingCooperativa;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao atualizar cooperativa.", ex);
            }
        }

        public async Task<Cooperativa?> DeleteAsync(int id)
        {
            try
            {
                var cooperativa = await _unitOfWork.CooperativaRepository.GetByIdAsync(id);
                if (cooperativa == null) return null;

                cooperativa.Ativo = false;
                _unitOfWork.CooperativaRepository.UpdateAsync(cooperativa);
                await _unitOfWork.CommitAsync();
                return cooperativa;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao deletar cooperativa.", ex);
            }
        }
    }
}