namespace interfaces;

public interface IRatingService
{
    Task ChangePlayerRatings(string usernameLeft, string usernameRight, bool leftWon);
}