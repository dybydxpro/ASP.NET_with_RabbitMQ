using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace TestMQ.API.Services.Contect;

public interface IMessageProducer
{
    Task SendingMessage<T>(T message);
}