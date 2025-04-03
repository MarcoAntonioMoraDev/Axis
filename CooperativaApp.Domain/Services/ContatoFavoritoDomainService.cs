using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Domain.Interfaces.Services;
using System;

namespace CooperativaApp.Domain.Services
{
    public class ContatoFavoritoDomainService : IContatoFavoritoDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContatoFavoritoDomainService(IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContatoFavorito?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ContatoFavoritoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ContatoFavorito>> GetAllAsync()
        {
            return await _unitOfWork.ContatoFavoritoRepository.GetAllAsync();
        }

        public async Task<ContatoFavorito> AddAsync(ContatoFavorito contatoFavorito)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var addedContatoFavorito = await _unitOfWork.ContatoFavoritoRepository.AddAsync(contatoFavorito);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return addedContatoFavorito;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao adicionar contato favorito.", ex);
            }
        }

        public async Task<ContatoFavorito?> UpdateAsync(ContatoFavorito contatoFavorito)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var existingContatoFavorito = await _unitOfWork.ContatoFavoritoRepository.GetByIdAsync(contatoFavorito.CodigoContatoFavoritoId);
                if (existingContatoFavorito == null)
                {
                    return null;
                }
                existingContatoFavorito.Nome = contatoFavorito.Nome;
                existingContatoFavorito.TipoChavePix = contatoFavorito.TipoChavePix;
                existingContatoFavorito.ChavePix = contatoFavorito.ChavePix;
                await _unitOfWork.ContatoFavoritoRepository.UpdateAsync(existingContatoFavorito);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return existingContatoFavorito;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao atualizar contato favorito.", ex);
            }
        }

        public async Task<ContatoFavorito?> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var contatoFavorito = await _unitOfWork.ContatoFavoritoRepository.GetByIdAsync(id);
                if (contatoFavorito == null)
                {
                    return null;
                }
                await _unitOfWork.ContatoFavoritoRepository.DeleteAsync(contatoFavorito.CodigoContatoFavoritoId);
                await _unitOfWork.SaveChangesAsync();
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