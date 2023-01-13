using System.Security.Claims;
using application;
using common;
using domain;
using infrastructure;
using interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace account.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : BaseController
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAccountService _accountService;
    
    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] UserLoginRequest userLoginRequest)
    {
        var token = await _accountService.Login(userLoginRequest);

        return Ok(token);
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup([FromForm] UserSignupRequest userSignupRequest)
    {
        await _accountService.Register(userSignupRequest);

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