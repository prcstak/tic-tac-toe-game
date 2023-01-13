using domain;
using infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace account.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly AccountContext _db;

    public ProfileController(ILogger<AuthenticationController> logger, AccountContext dbContext)
    {
        _logger = logger;
        _db = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayerProfileInfo(string name)
    {
        PlayerAccount? playerAccount = await _db.Players.FirstOrDefaultAsync(u => u.Name == name);
        if (playerAccount == null) return Unauthorized();
        return Ok(ProfileInfoDto.MapFrom(playerAccount));
    }
}