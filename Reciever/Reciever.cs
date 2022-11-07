using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

void main()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var rabbitConn = factory.CreateConnection();
    using (var channel = rabbitConn.CreateModel())
    {
        channel.QueueDeclare(queue:"hello", exclusive: false, durable:false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received: {message}");
        };
        channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
        Console.WriteLine("Press a key to finish process...");
        Console.ReadLine();
    }

    
}
main();