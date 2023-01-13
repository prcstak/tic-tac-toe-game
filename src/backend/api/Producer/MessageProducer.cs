using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace account.Producer;

public class MessageProducer
{
    private readonly IConfiguration _config;

    public MessageProducer(IConfiguration config)
    {
        _config = config;
    }
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ:Hostname"],
            Port = Convert.ToInt32(_config["RabbitMQ:Port"]),
        };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "game-ended-event",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(exchange: "",
            routingKey: "game-ended-event",
            basicProperties: null,
            body: body);
    }

    public void SendMessage<T>(T message, string queue)
    {
        throw new NotImplementedException();
    }
}