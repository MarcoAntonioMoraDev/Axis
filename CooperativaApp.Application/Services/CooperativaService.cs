using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace CooperativaApp.Application.Services
{
    public class CooperativaService : ICooperativaService
    {
        private readonly ICooperativaRepository _cooperativaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CooperativaService> _logger;

        public CooperativaService(ICooperativaRepository cooperativaRepository,
                                 IMapper mapper,
                                 IUnitOfWork unitOfWork,
                                 ILogger<CooperativaService> logger)
        {
            _cooperativaRepository = cooperativaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CooperativaDTOResponse?> GetByIdAsync(int id)
        {
            try
            {
                var cooperativa = await _cooperativaRepository.GetByIdAsync(id);
                return cooperativa == null || !cooperativa.Ativo ? null : _mapper.Map<CooperativaDTOResponse>(cooperativa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cooperativa by ID: {Id}", id);
                throw new Exception($"Erro ao obter cooperativa com ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<CooperativaDTOResponse>> GetAllAsync()
        {
            try
            {
                var cooperativas = await _cooperativaRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CooperativaDTOResponse>>(cooperativas.Where(c => c.Ativo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all cooperativas");
                throw new Exception("Erro ao obter todas as cooperativas.", ex);
            }
        }

        public async Task<CooperativaDTOResponse> AddAsync(CooperativaDTORequest cooperativaDTORequest)
        {
            try
            {
                var cooperativa = _mapper.Map<Cooperativa>(cooperativaDTORequest);
                await _unitOfWork.BeginTransactionAsync();
                var createdCooperativa = await _cooperativaRepository.AddAsync(cooperativa);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CooperativaDTOResponse>(createdCooperativa);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error adding cooperativa: {CooperativaRequest}", cooperativaDTORequest);
                throw new Exception("Erro ao adicionar cooperativa.", ex);
            }
        }


        public async Task<CooperativaDTOResponse?> UpdateAsync(int id, CooperativaDTORequest cooperativaDTORequest)
        {
            try
            {
                var cooperativa = await _cooperativaRepository.GetByIdAsync(id);
                if (cooperativa == null || !cooperativa.Ativo)
                {
                    _logger.LogWarning("Cooperativa não encontrada ou inativa. ID: {Id}", id);
                    return null;
                }

                _mapper.Map(cooperativaDTORequest, cooperativa); // AutoMapper faz o mapeamento

                await _unitOfWork.BeginTransactionAsync();
                await _cooperativaRepository.UpdateAsync(cooperativa);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CooperativaDTOResponse>(cooperativa);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Erro inesperado ao atualizar cooperativa. ID: {Id}", id);
                throw; // Re-lanca a exceção para ser tratada pelo controller
            }
        }

        public async Task<Cooperativa?> DeleteAsync(int id)
        {
            try
            {
                var cooperativa = await _cooperativaRepository.GetByIdAsync(id);
                if (cooperativa == null || !cooperativa.Ativo) return null;

                cooperativa.Ativo = false;
                await _unitOfWork.BeginTransactionAsync();
                await _cooperativaRepository.UpdateAsync(cooperativa);
                await _unitOfWork.CommitAsync();
                return cooperativa;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error deleting cooperativa: {Id}", id);
                throw new Exception($"Erro ao deletar cooperativa com ID {id}.", ex);
            }
        }
    }
}