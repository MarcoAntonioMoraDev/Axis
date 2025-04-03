using CooperativaApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class ContatoFavorito
{
    [Key]
    public int CodigoContatoFavoritoId { get; set; }
    public string? Nome { get; set; }
    public TipoChavePix TipoChavePix { get; set; }
    public string ChavePix { get; set; }
    public int CodigoCooperadoId { get; set; }
    public Cooperado? Cooperado { get; set; }
}

public enum TipoChavePix
{
    CPF = 1,
    CNPJ = 2,
    Email = 3,
    Telefone = 4,
    Aleatoria = 5
}