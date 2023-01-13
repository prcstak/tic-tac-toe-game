using System.Text.RegularExpressions;
using account.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace account.Hubs;

[Authorize]
public class GameHub : Hub
{
    private readonly Game.Game _game;
    
    public GameHub(Game.Game game)
    {
        _game = game;
    }
    
    private string Username => Context!.User!.Identity!.Name!;

    public void JoinGame(string roomName)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        _game.JoinGame(
            username: Username, 
            connectionId: Context.ConnectionId, 
            roomName: roomName);
    }

    public void CreateGame(int maxRating)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, Username);
        _game.CreateGame(Username, maxRating);
    }

    public void Leave(string roomName)
    {
        Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        _game.RemoveUserFromGame(roomName, Username);
    }

    public void OnTurn(CellType cellType, int index, string roomName)
    {
        _game.MakeMove(cellType, index, roomName);

        var status = _game.GetGameStatus(roomName);
        if (status is GameInfo.GameStatus.End or GameInfo.GameStatus.Tie)
            Clients.Group(roomName).SendAsync("GameEnded", status);
    }
}