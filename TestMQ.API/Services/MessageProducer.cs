using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using TestMQ.API.Services.Contect;

namespace TestMQ.API.Services;

public class MessageProducer : IMessageProducer
{
    protected readonly IConfiguration _configuration;
    
    public MessageProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:HostName"],
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"],
            VirtualHost = _configuration["RabbitMQ:VirtualHost"]
        };

        using var conn = await factory.CreateConnectionAsync();
        
        using var channel = await conn.CreateChannelAsync();
        
        channel.QueueDeclareAsync("booking", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublishAsync("", "booking", body: body);
    }
}