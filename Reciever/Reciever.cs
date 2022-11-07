using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

void main()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var rabbitConn = factory.CreateConnection();
    using (var channel = rabbitConn.CreateModel())
    {
        channel.ExchangeDeclare(exchange:"logs", type: ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue:queueName, exchange:"logs", routingKey:"");
        
        Console.WriteLine("Waiting for logs...");
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received: {message}");
        };
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine("Press a key to finish process...");
        Console.ReadLine();
    }

    
}
main();