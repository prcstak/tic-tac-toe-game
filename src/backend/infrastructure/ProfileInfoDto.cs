using domain;

namespace infrastructure;

public record ProfileInfoDto(string name, int Win, int Loss)
{
    public static ProfileInfoDto
        MapFrom(PlayerAccount playerAccount)
        => new(playerAccount.Name, playerAccount.Win, playerAccount.Loss);
};