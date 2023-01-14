using System.Text.RegularExpressions;
using account.Game;
using account.Producer;
using common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace account.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GameHub : Hub
{
    private readonly Game.Game _game;
    private readonly MessageProducer _messageProducer;

    public GameHub(Game.Game game, MessageProducer messageProducer)
    {
        _game = game;
        _messageProducer = messageProducer;
    }

    private string Username => Context!.UserIdentifier!;

    public void JoinGame(string roomName)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        _game.JoinGame(
            username: Username,
            connectionId: Context.ConnectionId,
            roomName: roomName);
        
        Clients.Group(roomName)
            .SendAsync("OnTurn",
                _game.GetTable(roomName),
                (int) _game.GetTurn(roomName));
    }

    public void CreateGame(int maxRating)
    {
        Groups.AddToGroupAsync(Context.ConnectionId, Username);
        _game.CreateGame(Username, maxRating);
        GetGames();
    }

    public void Leave(string roomName)
    {
        Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        _game.RemoveUserFromGame(roomName, Username);
    }

    public void GetGames()
    {
        Clients.All.SendAsync("Games", _game.GetGames());
    }

    public void MakeTurn(CellType cellType, int index, string roomName)
    {
        _game.MakeMove(cellType, index, roomName);

        var status = _game.GetGameStatus(roomName);
        if (status is GameInfo.GameStatus.End or GameInfo.GameStatus.Tie)
        {
            Clients.Group(roomName)
                .SendAsync("OnTurn",
                    _game.GetTable(roomName)
                    , _game.GetTurn(roomName));
            Clients.Group(roomName).SendAsync("GameEnded", status, cellType);
            var playingPlayers = _game.GetPlayingPlayers(roomName);
            var leftWon = Context.ConnectionId == playingPlayers[0].ConnectionId;
            _messageProducer.SendMessage(new GameEndedEvent(
                playingPlayers[0].Username, playingPlayers[1].Username, leftWon));
        }

        Clients.Group(roomName)
            .SendAsync("OnTurn",
                _game.GetTable(roomName)
                , _game.GetTurn(roomName));
    }

    public void ClearTable(string roomName)
    {
        _game.Restart(roomName);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _game.RemoveFromAllRooms(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}