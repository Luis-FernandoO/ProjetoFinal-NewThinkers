using GerenciadorDeProdutos.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeProdutos.Models;

public class Usuario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome de para registro é obrigatório!")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "CPF é Obrigatório!")]
    public string CPF { get; set; } = string.Empty;
    [Required(ErrorMessage = "Cargo é Obrigatório!")]
    [Range(1, 2, ErrorMessage = "Cargo inválido. Selecione um cargo válido.")]
    public Cargos Cargo { get; set; }
    [Required(ErrorMessage = "Senha é Obrigatória!")]
    public string Senha { get; set; } = string.Empty;
}
