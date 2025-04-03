using System.ComponentModel.DataAnnotations;

namespace CooperativaApp.Domain.Entities
{
    public class Cooperativa
    {
        [Key]
        public int CodigoCooperativaId { get; set; }
        public string? Nome { get; set; }
        public bool Ativo { get; set; }

        public List<Cooperado>? Cooperados { get; set; }
    }
}
