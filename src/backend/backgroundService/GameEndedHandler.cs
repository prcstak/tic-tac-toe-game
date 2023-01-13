using System.Text.Json;
using common;
using interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace backgroundService;

public class GameEndedHandler : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private ConnectionFactory _connectionFactory;
    private readonly string _queueName;
    private readonly IConfiguration _config;
    private ILogger<GameEndedHandler> _logger;
    private readonly IRatingService _ratingService;

    protected GameEndedHandler(
        IConfiguration config,
        ILogger<GameEndedHandler> logger,
        IRatingService ratingService)
    {
        _config = config;
        _queueName = "game-ended-event";
        _logger = logger;
        _ratingService = ratingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var gameEndedEvent = JsonSerializer.Deserialize<GameEndedEvent>(body);
                await _ratingService.ChangePlayerRatings(
                    gameEndedEvent.LeftPlayer,
                    gameEndedEvent.RightPlayer,
                    gameEndedEvent.leftWon);

            }
            catch (Exception exception)
            {
                _logger.LogWarning("Exception: " + exception.Message);
            }
        };
        
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        
        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        
        _logger.LogInformation($"[{_queueName}] has stopped.");
    }
    
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ:Hostname"],
            Port = Convert.ToInt32(_config["RabbitMQ:Port"]),
        };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        _logger.LogInformation($"[{_queueName}] has started.");

        return base.StartAsync(cancellationToken);
    }
}