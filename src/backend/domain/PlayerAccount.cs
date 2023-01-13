using System.ComponentModel.DataAnnotations;

namespace domain;

public record PlayerAccount(Guid Id, string Name, string Password, int Win, int Loss );