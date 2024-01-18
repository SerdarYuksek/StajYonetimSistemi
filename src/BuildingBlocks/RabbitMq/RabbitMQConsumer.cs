using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMq
{
    public class RabbitMQConsumer
    {
        private readonly IModel _channel;

        public RabbitMQConsumer(IModel channel)
        {
            _channel = channel;
        }
        public void ConsumeMessages(string exchange, string queueName, string routingKey, Action<string> handleMessage)
        {
            _channel.ExchangeDeclare(exchange, ExchangeType.Direct);
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                handleMessage(message);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
