// See https://aka.ms/new-console-template for more information


using System.Text;
using RabbitMQ.Client;

void main()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var rabbitConn = factory.CreateConnection();
    using (var channel = rabbitConn.CreateModel())
    {
        // Creating a Queue
        channel.QueueDeclare(queue: "hello", exclusive: false, arguments: null, autoDelete: false);
        const string message = "Send says: Hello world";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
        Console.WriteLine("[x] Message sent");
    }
    Console.WriteLine("Press any  key to finish process...");
    Console.ReadLine();
}
main();
