using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GerenciadorDeProdutos.Data;
using GerenciadorDeProdutos.Models;
using GerenciadorDeProdutos.Enums;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("ConsultarTodosProdutos")]
    public IActionResult ObterTodosProdutos()
    {
        var produtos = _context.Produtos.ToList();
        return Ok(produtos);
    }

    [HttpGet("ConsultarProdutosEmEstoque")]
    public IActionResult ObterProdutosEmEstoque()
    {
        var produtosEmEstoque = _context.Produtos
            .Where(p => p.Status == StatusProduto.EmEstoque).ToList();

        return Ok(produtosEmEstoque);
    }


    [Authorize(Roles = "Funcionario,Gerente")]
    [HttpPost("CadastrandoProduto")]
    public IActionResult CadastrandoProduto([FromBody] Produtos produto)
    {
        if (string.IsNullOrWhiteSpace(produto.Nome) || produto.Preco <= 0 || produto.QuantidadeEmEstoque < 0)
            return BadRequest("Dados inválidos para o produto.");

        produto.Status = produto.QuantidadeEmEstoque > 0 ? StatusProduto.EmEstoque : StatusProduto.Indisponivel;

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(ObterTodosProdutos), new { id = produto.Id }, produto);
    }

    [Authorize(Roles = "Gerente")]
    [HttpDelete("{id}")]
    public IActionResult RemoverProdutos(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
        if (produto == null)
            return NotFound("Produto não encontrado.");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok("Produto excluído com sucesso.");
    }
    [Authorize(Roles = "Funcionario,Gerente")]
    [HttpPut("AtualizarEstoque/{id}")]
    public IActionResult AtualizarEstoque(int id, [FromBody] int quantidade)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
        if (produto == null)
            return NotFound("Produto não encontrado.");

        produto.QuantidadeEmEstoque += quantidade; 

        _context.Produtos.Update(produto);
        _context.SaveChanges();

        return Ok($"Estoque do produto {produto.Nome} atualizado com sucesso.");
    }
}
