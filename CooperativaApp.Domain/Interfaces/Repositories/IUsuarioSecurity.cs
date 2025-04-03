using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioSecurity
    {
        string CreateToken(Usuario usuario);
    }
}
