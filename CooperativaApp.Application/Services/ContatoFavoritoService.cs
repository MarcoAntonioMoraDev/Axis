using AutoMapper;
using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooperativaApp.Application.Services
{
    public class ContatoFavoritoService : IContatoFavoritoService
    {
        private readonly IContatoFavoritoRepository _contatoFavoritoRepository;
        private readonly ICooperadoRepository _cooperadoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContatoFavoritoService(IContatoFavoritoRepository contatoFavoritoRepository,
                                       ICooperadoRepository cooperadoRepository,
                                       IMapper mapper,
                                       IUnitOfWork unitOfWork)
        {
            _contatoFavoritoRepository = contatoFavoritoRepository;
            _cooperadoRepository = cooperadoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContatoFavoritoDTOResponse?> GetByIdAsync(int id)
        {
            var contatoFavorito = await _contatoFavoritoRepository.GetByIdAsync(id);
            return contatoFavorito == null ? null : _mapper.Map<ContatoFavoritoDTOResponse>(contatoFavorito);
        }

        public async Task<IEnumerable<ContatoFavoritoDTOResponse>> GetAllAsync()
        {
            var contatosFavoritos = await _contatoFavoritoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContatoFavoritoDTOResponse>>(contatosFavoritos);
        }

        public async Task<ContatoFavoritoDTOResponse> AddAsync(ContatoFavoritoDTORequest contatoFavoritoDTORequest)
        {
            var cooperado = await _cooperadoRepository.GetByIdAsync(contatoFavoritoDTORequest.CodigoCooperadoId);
            if (cooperado == null || !cooperado.Ativo)
            {
                throw new Exception("Cooperado não encontrado ou inativo.");
            }

            var contatoFavorito = _mapper.Map<ContatoFavorito>(contatoFavoritoDTORequest);
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var createdContatoFavorito = await _contatoFavoritoRepository.AddAsync(contatoFavorito);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ContatoFavoritoDTOResponse>(createdContatoFavorito);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao adicionar contato favorito.", ex);
            }
        }

        public async Task UpdateAsync(ContatoFavoritoDTORequest contatoFavoritoDto)
        {
            var contatoFavorito = _mapper.Map<ContatoFavorito>(contatoFavoritoDto);
            await _contatoFavoritoRepository.UpdateAsync(contatoFavorito);
        }

        public async Task<ContatoFavorito?> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var contatoFavorito = await _contatoFavoritoRepository.GetByIdAsync(id);
                if (contatoFavorito == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return null;
                }

                await _contatoFavoritoRepository.DeleteAsync(contatoFavorito.CodigoContatoFavoritoId);
                await _unitOfWork.CommitAsync();
                return contatoFavorito;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao deletar contato favorito.", ex);
            }
        }
    }
}