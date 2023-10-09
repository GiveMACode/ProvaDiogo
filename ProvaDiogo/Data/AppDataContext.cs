using ProvaDiogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ProvaDiogo.Data;
public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options){ }

    //Classes que vão se tornar tabelas no banco de dados
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }
}
