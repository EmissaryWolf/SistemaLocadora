using Microsoft.EntityFrameworkCore;
using SistemaLocadora; // Adicione esta linha para ele enxergar a classe Veiculo

public class AppDbContext : DbContext
{
    public DbSet<Veiculo> Veiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=locadora.db");
    }
}