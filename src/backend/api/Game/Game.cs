using System.Collections.Concurrent;

namespace account.Game;

public class UserInfo
{
    public string Username { get; set; }
    public string ConnectionId { get; set; }
    public PlayerType Type { get; set; }
}

public enum PlayerType
{
    Player,
    Spectator
}

public class GameInfo
{
    public enum GameStatus
    {
        InProgress,
        Tie,
        End
    }
    
    public string Owner { get; set; }
    public DateTime DateCreated { get; set; }
    public int MaxRating { get; set; }
    public Status Status { get; set; }
    public List<UserInfo> UserList { get; set; }
    public CellType[] Table { get; set; } = new[]
    {
        CellType.Empty, CellType.Empty, CellType.Empty,
        CellType.Empty, CellType.Empty, CellType.Empty,
        CellType.Empty, CellType.Empty, CellType.Empty,
    };

    public GameStatus GetStatus()
    {
        var isWin =
            CheckRow(new CellType[] { Table[0], Table[1], Table[2] }) || // borders
            CheckRow(new CellType[] { Table[2], Table[5], Table[6] }) ||
            CheckRow(new CellType[] { Table[8], Table[7], Table[6] }) ||
            CheckRow(new CellType[] { Table[6], Table[3], Table[0] }) ||

            CheckRow(new CellType[] { Table[3], Table[4], Table[5] }) || // center
            CheckRow(new CellType[] { Table[1], Table[4], Table[7] }) ||

            CheckRow(new CellType[] { Table[0], Table[4], Table[8] }) || // diagonal
            CheckRow(new CellType[] { Table[2], Table[4], Table[6] });

        var isTie = !isWin && Table.All(cell => cell != CellType.Empty);

        return isWin
            ? GameStatus.End
            : isTie
                ? GameStatus.Tie
                : GameStatus.InProgress;
    }

    private bool CheckRow(CellType[] cells)
    {
        return cells.All(cell => cells[0] == cell && cell != CellType.Empty);
    }
};

public enum Status
{
    Started,
    NotStarted
}

public enum CellType
{
    Empty,
    X,
    O
}

public class Game
{
    private static readonly ConcurrentDictionary<string, GameInfo> _games = new();

    public void CreateGame(string ownerName, int maxRating)
    {
        _games[ownerName] = new GameInfo
        {
            Owner = ownerName,
            DateCreated = DateTime.Now,
            MaxRating = maxRating,
            Status = Status.NotStarted,
            UserList =  new List<UserInfo>()
        };
    }

    public void JoinGame(string username, string connectionId, string roomName)
    {
        _games[roomName].UserList.Add(new UserInfo
        {
            Username = username,
            ConnectionId = connectionId,
            Type = _games[roomName].UserList.Count == 1 ? PlayerType.Player : PlayerType.Spectator
        });
    }

    public void RemoveUserFromGame(string roomName, string username)
    {
        _games[roomName].UserList.Remove(_games[roomName].UserList.First(user => user.Username == username));
        if (_games[roomName].UserList.Count > 1)
            _games[roomName].UserList[1].Type = PlayerType.Spectator;

        if (_games[roomName].UserList.Count == 1)
            _games[roomName].Status = Status.NotStarted;
    }

    public void MakeMove(CellType cellType, int index, string roomName)
    {
        _games[roomName].Table[index] = cellType;
    }

    public GameInfo.GameStatus GetGameStatus(string roomName)
    {
        return _games[roomName].GetStatus();
    }

    public void Restart(string roomName)
    {
        for (var i = 0; i < _games[roomName].Table.Length; i++)
        {
            _games[roomName].Table[i] = CellType.Empty;
        }
    }
}