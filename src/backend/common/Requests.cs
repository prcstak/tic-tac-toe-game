namespace common;

public record UserSignupRequest(string Username, string Password);

public record UserLoginRequest(string Username, string Password);