using System.ComponentModel.DataAnnotations;

namespace CooperativaApp.Application.DTOs
{
    public class ContatoFavoritoDTORequest
    {
        
        [Required(ErrorMessage = "O nome do contato favorito é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome do contato favorito não pode exceder 150 caracteres.")]
        public string? Nome { get; set; }
        public int TipoChavePix { get; set; }
        public string ChavePix { get; set; }
        public int CodigoCooperadoId { get; set; }
    }
}