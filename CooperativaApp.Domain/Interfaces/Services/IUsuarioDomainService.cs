using CooperativaApp.Domain.Entities;

namespace CooperativaApp.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        Task Create(Usuario usuario);

        Task<string> Authenticate(string email, string senha);
    }
}
