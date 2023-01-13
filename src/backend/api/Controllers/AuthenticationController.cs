using System.Security.Claims;
using domain;
using infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace account.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly AccountContext _db;

    public AuthenticationController(ILogger<AuthenticationController> logger, AccountContext dbContext)
    {
        _logger = logger;
        _db = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Login(string name, string password)
    {
        PlayerAccount? playerAccount = await _db.Players.FirstOrDefaultAsync(u =>
            u.Name == name && u.Password == password); //TODO: add hash 
        if (playerAccount == null) return Unauthorized();
        
        await Authenticate(name);
        _logger.LogInformation($"Player {name} logged in");
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Signup(string name, string password)
    {
        PlayerAccount? playerAccount = await _db.Players.FirstOrDefaultAsync(u =>
            u.Name == name);
        if (playerAccount != null) return Conflict();
        
        _db.Players.Add(new PlayerAccount(Guid.NewGuid(), name, password, 0, 0)); //TODO: add hash
        await _db.SaveChangesAsync();
        await Authenticate(name);
        _logger.LogInformation($"Player {name} signed up");
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    private async Task Authenticate(string userName)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
        };
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
}