using Microsoft.EntityFrameworkCore;
using ProvaDiogo.Models;

namespace ProvaDiogo.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    //public DbSet<Usuario> Usuario { get; set; }

}
