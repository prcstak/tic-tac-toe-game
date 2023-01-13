using interfaces;
using Microsoft.EntityFrameworkCore;

namespace backgroundService.Services;

public class RatingService : IRatingService
{
    private readonly IApplicationDbContext _context;

    public RatingService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ChangePlayerRatings(string usernameLeft, string usernameRight, bool leftWon)
    {
        var left = await _context.Players.FirstOrDefaultAsync(player => player.UserName == usernameLeft);
        if (left == null)
            throw new ArgumentException($"No user with {usernameLeft} name");
        
        var right = await _context.Players.FirstOrDefaultAsync(player => player.UserName == usernameRight);
        if (left == null)
            throw new ArgumentException($"No user with {usernameRight} name");

        if (leftWon)
        {
            left.Rating += 3;
            right.Rating -= 1;
        }
        else
        {
            left.Rating -= 1;
            right.Rating += 3;
        }

        await _context.SaveChangesAsync();
    }
}