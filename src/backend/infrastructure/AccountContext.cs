using domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure;

public class AccountContext : DbContext
{
    public DbSet<PlayerAccount> Players { get; set;}
    public AccountContext(DbContextOptions<AccountContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}