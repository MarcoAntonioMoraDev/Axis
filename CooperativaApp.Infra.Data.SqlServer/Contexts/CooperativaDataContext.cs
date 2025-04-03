using CooperativaApp.Domain.Entities;
using CooperativaApp.Infra.Data.SqlServer.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApp.Infra.Data.SqlServer.Contexts
{
    public class CooperativaDataContext : DbContext
    {
        public CooperativaDataContext(DbContextOptions<CooperativaDataContext> options) : base(options) { }

        public DbSet<ContatoFavorito> ContatosFavoritos { get; set; }
        public DbSet<Cooperado> Cooperados { get; set; }
        public DbSet<Cooperativa> Cooperativas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoFavoritoMap());
            modelBuilder.ApplyConfiguration(new CooperadoMap());
            modelBuilder.ApplyConfiguration(new CooperativaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}