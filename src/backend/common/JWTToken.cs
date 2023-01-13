namespace common;

public record JWTToken(string AccessToken, DateTime StartsAt, DateTime ExpiresAt);