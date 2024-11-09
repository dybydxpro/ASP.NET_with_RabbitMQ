using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "mypassword",
    VirtualHost = "/"
};

using var conn = await factory.CreateConnectionAsync();

using var channel = await conn.CreateChannelAsync();

channel.QueueDeclareAsync("booking", durable: true, exclusive: false, autoDelete: false, arguments: null);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};

channel.BasicConsumeAsync("booking", true, consumer);

Console.WriteLine(" [*] Waiting for messages. Press [Enter] to exit.");

Console.ReadKey();
