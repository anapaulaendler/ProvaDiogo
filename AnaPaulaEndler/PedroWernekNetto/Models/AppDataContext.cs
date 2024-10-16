using Microsoft.EntityFrameworkCore;

namespace PedroWernekNetto;

public class AppDataContext : DbContext
{
    public DbSet<Funcionario> TabelaFuncionarios { get; set; }
    public DbSet<Folha> TabelaFolhas { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=anapaulaendler_pedrowerneknetto.db");
    }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    // modelBuilder.Entity<Funcionario>()
    //     .HasMany(e => e.Folhas)
    //     .WithOne(e => e.Funcionario)
    //     .HasForeignKey(e => e.FuncionarioId)
    //     .IsRequired();
    // }

}
