using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GerenciadorDeProdutos.Data;
using GerenciadorDeProdutos.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly string _key = "chave-de-seguranca-super-gerenciador"; 

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Register([FromBody] Usuario usuario)
    {
        if (_context.Usuarios.Any(u => u.CPF == usuario.CPF))
            return BadRequest("CPF já registrado.");

        usuario.Senha = ComputeSha256Hash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.CPF == request.CPF);

        if (usuario == null || usuario.Senha != ComputeSha256Hash(request.Senha))
            return Unauthorized("CPF ou senha inválidos.");

        var token = GenerateToken(usuario);
        return Ok(new { Token = token });
    }

    private string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Cargo.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private string ComputeSha256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
public class LoginRequest
{
    public string CPF { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
