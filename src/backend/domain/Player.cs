using Microsoft.AspNetCore.Identity;

namespace domain;

public class Player : IdentityUser
{
    public int Rating { get; set; }
}