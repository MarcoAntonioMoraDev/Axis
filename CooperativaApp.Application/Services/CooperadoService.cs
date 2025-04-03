using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using AutoMapper;

namespace CooperativaApp.Application.Services
{
    public class CooperadoService : ICooperadoService
    {
        private readonly ICooperadoRepository _cooperadoRepository;
        private readonly ICooperativaRepository _cooperativaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CooperadoService(ICooperadoRepository cooperadoRepository,
                                ICooperativaRepository cooperativaRepository,
                                IMapper mapper,
                                IUnitOfWork unitOfWork)
        {
            _cooperadoRepository = cooperadoRepository;
            _cooperativaRepository = cooperativaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CooperadoDTOResponse?> GetByIdAsync(int id)
        {
            var cooperado = await _cooperadoRepository.GetByIdAsync(id);
            return cooperado == null || !cooperado.Ativo ? null : _mapper.Map<CooperadoDTOResponse>(cooperado);
        }

        public async Task<IEnumerable<CooperadoDTOResponse>> GetByNomeAsync(string nome)
        {
            var cooperados = await _cooperadoRepository.GetByNomeAsync(nome);
            return _mapper.Map<IEnumerable<CooperadoDTOResponse>>(cooperados.Where(c => c.Ativo));
        }

        public async Task<IEnumerable<CooperadoDTOResponse>> GetByContaAsync(string conta)
        {
            var cooperados = await _cooperadoRepository.GetByContaAsync(conta);
            return _mapper.Map<IEnumerable<CooperadoDTOResponse>>(cooperados.Where(c => c.Ativo));
        }

        public async Task<IEnumerable<CooperadoDTOResponse>> GetAllAsync()
        {
            var cooperados = await _cooperadoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CooperadoDTOResponse>>(cooperados.Where(c => c.Ativo));
        }

        public async Task<CooperadoDTOResponse> AddAsync(CooperadoDTORequest cooperadoDTORequest)
        {
            var cooperativa = await _cooperativaRepository.GetByIdAsync(cooperadoDTORequest.CodigoCooperativaId);
            if (cooperativa == null || !cooperativa.Ativo || cooperativa.CodigoCooperativaId == null)
            {
                throw new Exception("Cooperativa não encontrada ou inativa.");
            }

            var cooperado = _mapper.Map<Cooperado>(cooperadoDTORequest);
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var createdCooperado = await _cooperadoRepository.AddAsync(cooperado);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CooperadoDTOResponse>(createdCooperado);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao adicionar cooperado.", ex);
            }
        }

        public async Task UpdateAsync(CooperadoDTOResponse cooperadoDto)
        {
            var cooperado = _mapper.Map<Cooperado>(cooperadoDto);
            await _cooperadoRepository.UpdateAsync(cooperado);
        }

        public async Task<Cooperado?> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var cooperado = await _cooperadoRepository.GetByIdAsync(id);
                if (cooperado == null || !cooperado.Ativo)
                {
                    await _unitOfWork.RollbackAsync();
                    return null;
                }

                cooperado.Ativo = false;
                await _cooperadoRepository.UpdateAsync(cooperado);

                var cooperativa = await _cooperativaRepository.GetByIdAsync(cooperado.CodigoCooperativaId);
                if (cooperativa != null && cooperativa.Cooperados != null)
                {
                    cooperativa.Cooperados.Remove(cooperado);
                    await _cooperativaRepository.UpdateAsync(cooperativa);
                }

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