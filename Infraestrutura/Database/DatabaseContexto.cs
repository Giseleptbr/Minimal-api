using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Database;

public class DatabaseContexto : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "Administrador@teste.com",
                Senha = "123456",
                Perfil = "Administrador"
            }
        );

        base.OnModelCreating(modelBuilder); // ✅ garante que o EF configure as tabelas padrão
    }

    public DatabaseContexto(DbContextOptions<DatabaseContexto> options) : base(options)
    {
    }

    // DbSets para as tabelas
    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;  // ✅ adicione esta linha
}
