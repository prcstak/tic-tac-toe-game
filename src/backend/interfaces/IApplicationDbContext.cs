using domain;
using Microsoft.EntityFrameworkCore;

namespace interfaces;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}