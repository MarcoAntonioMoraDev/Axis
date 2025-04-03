using System.ComponentModel.DataAnnotations;

namespace CooperativaApp.Application.DTOs
{
    public class CooperadoDTORequest
    {
        [Required(ErrorMessage = "O nome do Cooperado é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome do Cooperado não pode exceder 150 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O número do banco é obrigatório.")]
        [StringLength(3, ErrorMessage = "O número do banco não pode exceder 3 caracteres.")]
        public string? Banco { get; set; }
        
        [Required(ErrorMessage = "O número da agência é obrigatório.")]
        [StringLength(5, ErrorMessage = "O número da agência não pode exceder 5 caracteres.")]
        public string? Agencia { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [StringLength(7, ErrorMessage = "O número da conta não pode exceder 7 caracteres.")]
        public string? Conta { get; set; }

        [Required(ErrorMessage = "O dígito verificador é obrigatório.")]
        [StringLength(2, ErrorMessage = "O dígito verificador não pode exceder 2 caracteres.")]
        public string? DigitoVerificador { get; set; }
        
        [PositiveIntegerAttributeValidation(ErrorMessage = "Relacione o cooperado a uma cooperativa válida")]
        public int CodigoCooperativaId { get; set; }
    }
}