using common;

namespace interfaces;

public interface IAccountService
{
    Task<JWTToken> Login(UserLoginRequest userLoginRequest, CancellationToken cancellationToken = default);
    Task Authenticate(); // todo return claims
    Task Register(UserSignupRequest userSignupRequest, CancellationToken cancellationToken = default);
}