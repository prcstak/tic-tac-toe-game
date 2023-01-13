using common;

namespace interfaces;

public interface ITokenService
{
    JWTToken GenerateToken(string username);
}