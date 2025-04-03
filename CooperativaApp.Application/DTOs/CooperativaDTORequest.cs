using System.ComponentModel.DataAnnotations;

namespace CooperativaApp.Application.DTOs
{
    public class CooperativaDTORequest
    {
        public int? CodigoCooperativaId { get; set; }

        [Required(ErrorMessage = "O nome da cooperativa é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome da cooperativa não pode exceder 150 caracteres.")]
        public string Nome { get; set; } = null!;
    }
}