using common;
using domain;
using interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace application;

public class AccountService : IAccountService
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<Player> _userManager;
    private readonly SignInManager<Player> _signInManager;
    private readonly ILogger<AccountService> _logger;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public AccountService(
        IApplicationDbContext context,
        ILogger<AccountService> logger,
        UserManager<Player> userManager,
        SignInManager<Player> signInManager,
        IConfiguration configuration,
        ITokenService tokenService)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Register(UserSignupRequest userSignupRequest, CancellationToken cancellationToken = default)
    {
        var newUser = new Player
        {
            UserName = userSignupRequest.Username
        };

        var result = await _userManager.CreateAsync(newUser, userSignupRequest.Password);
        if (!result.Succeeded)
        {
            var passwordTooShortException = result.Errors.FirstOrDefault(a => a.Code == "PasswordTooShort");
            if (passwordTooShortException != null)
                throw new PasswordTooShortException(passwordTooShortException.Description);
            else throw new UserAlreadyExistsException(userSignupRequest.Username);
        }
    }

    public async Task<JWTToken> Login(UserLoginRequest userLoginRequest, CancellationToken cancellationToken = default)
    {
        var result = await _signInManager.PasswordSignInAsync(userLoginRequest.Username, userLoginRequest.Password, false, false);
        if (!result.Succeeded)
            throw new WrongCredentialsException();

        var jwtToken = _tokenService.GenerateToken(userLoginRequest.Username);

        return jwtToken;
    }

    public Task Authenticate()
    {
        throw new NotImplementedException();
    }
}