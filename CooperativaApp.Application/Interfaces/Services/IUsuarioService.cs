using CooperativaApp.Application.DTOs;
using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTOResponse> Create(UsuarioDTORequest request);

        Task<LoginDTOResponse> Login(LoginDTORequest request);
    }
}
