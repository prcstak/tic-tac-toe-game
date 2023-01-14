using System.Security.Claims;
using common;
using interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public PlayerController(
        IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet]
    [Route("rating")]
    public async Task<IActionResult> GetRating()
    {

        var username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var rating = await _ratingService
            .GetPlayerRating(username);

        return Ok(new PlayerInfo(username, rating));
    }
}