using System.Runtime.Intrinsics.Arm;

namespace common;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string username) : base($"User with {username} already exists") { }
}

public class WrongCredentialsException : Exception
{
    public WrongCredentialsException() : base($"User with such credentials does not exist") { }
}