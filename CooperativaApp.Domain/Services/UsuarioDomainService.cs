using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Helpers;
using CooperativaApp.Domain.Interfaces.Repositories;
using CooperativaApp.Domain.Interfaces.Services;

namespace CooperativaApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioSecurity _usuarioSecurity;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IUsuarioSecurity usuarioSecurity)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioSecurity = usuarioSecurity;
        }

        public async Task Create(Usuario usuario)
        {
            #region Verificar se o email já está cadastrado

            if (await _usuarioRepository.AnyAsync(usuario.Email))
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            #endregion

            #region Criptografar a senha

            usuario.Senha = SHA256Helper.Encrypt(usuario.Senha);

            #endregion

            #region Gravar o usuário no banco de dados

            _usuarioRepository?.AddAsync(usuario);

            #endregion
        }

        public async Task<string> Authenticate(string email, string senha)
        {
            #region Buscar o usuário no banco de dados através do email e da senha

            var usuario = await _usuarioRepository.FindAsync(email, SHA256Helper.Encrypt(senha));
            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado. Verifique o ID informado.");

            #endregion

            #region Gerando e retornando o TOKEN JWT do usuário

            return _usuarioSecurity.CreateToken(usuario);

            #endregion
        }
    }
}
