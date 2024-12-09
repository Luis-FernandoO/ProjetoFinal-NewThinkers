using GerenciadorDeProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeProdutos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options ){}

    public  DbSet<Produtos> Produtos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

 


}
