namespace CooperativaApp.Application.DTOs
{
    public class CooperadoDTOResponse
    {
        public int CodigoCooperadoId { get; set; }
        public string? Nome { get; set; }
        public string? Banco { get; set; }
        public string? Agencia { get; set; }
        public string? Conta { get; set; }
        public string? DigitoVerificador { get; set; }
        public bool Ativo { get; set; }
        public int CodigoCooperativaId { get; set; }
    }
}