namespace CooperativaApp.Application.DTOs
{
    public class CooperativaDTOResponse
    {
        public int CodigoCooperativaId { get; set; }
        public string? Nome { get; set; }
        public bool Ativo { get; set; }
        public List<CooperadoDTOResponse>? Cooperados { get; set; }
    }
}