using Aula7.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Aula7.Api;

public class EfContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Manager> Managers { get; }

    public EfContext(DbContextOptions<EfContext> dbContextOptions) :base(dbContextOptions)
    {
        
    }
}