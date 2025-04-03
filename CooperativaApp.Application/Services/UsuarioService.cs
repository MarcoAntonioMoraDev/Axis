using AutoMapper;
using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Services;

namespace CooperativaApp.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioDomainService usuarioDomainService, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
        }

        public async Task<UsuarioDTOResponse> Create(UsuarioDTORequest request)
        {
            var usuario = _mapper.Map<Usuario>(request);

            _usuarioDomainService.Create(usuario);

            return _mapper.Map<UsuarioDTOResponse>(usuario);
        }

        public async Task<LoginDTOResponse> Login(LoginDTORequest request)
        {
            var result = new LoginDTOResponse
            {
                Token = await _usuarioDomainService.Authenticate(request.Email, request.Senha)
            };

            return result;
        }
    }
}
