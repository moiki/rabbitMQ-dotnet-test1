// See https://aka.ms/new-console-template for more information


using System.Text;
using RabbitMQ.Client;

void main()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var rabbitConn = factory.CreateConnection();
    using (var channel = rabbitConn.CreateModel())
    {
        // Creating Exchange
        channel.ExchangeDeclare(exchange:"logs", type: ExchangeType.Fanout);
        // channel.QueueDeclare(queue: "hello", exclusive: false, arguments: null, autoDelete: false);
        var message = getMessage();
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);
        Console.WriteLine("[x] Message sent");
    }
    Console.WriteLine("Press any  key to finish process...");
    Console.ReadLine();
}

string getMessage()
{
    return "info: Hello World!";
}

main();
