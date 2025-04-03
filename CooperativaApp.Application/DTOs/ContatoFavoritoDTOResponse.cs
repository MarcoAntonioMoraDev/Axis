namespace CooperativaApp.Application.DTOs
{
    public class ContatoFavoritoDTOResponse
    {
        public int CodigoContatoFavoritoId { get; set; }
        public string? Nome { get; set; }
        public string TipoChavePix { get; set; }
        public string ChavePix { get; set; }
        public int CodigoCooperadoId { get; set; }
    }
}