using domain;

namespace interfaces;

public interface IRatingService
{
    Task ChangePlayerRatings(string usernameLeft, string usernameRight, bool leftWon);
    Task<int> GetPlayerRating(string username);
}