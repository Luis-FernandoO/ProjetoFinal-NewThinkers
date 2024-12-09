using GerenciadorDeProdutos.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeProdutos.Models;

public class Produtos
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    public string Nome { get; set; }
    public string Descricao { get; set; }
    [Required(ErrorMessage = "O status do produto é obrigatório.")]
    [EnumDataType(typeof(StatusProduto), ErrorMessage = "Status inválido. O status deve ser 'Em Estoque' ou 'Indisponível'.")]
    public StatusProduto Status { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco {  get; set; }
    public int QuantidadeEmEstoque { get; set; }

}
