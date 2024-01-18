using RabbitMQ.Client;
using System.Text;

namespace RabbitMq
{
    public class RabbitMQPublisher
    {
        private readonly IModel _channel;

        public RabbitMQPublisher(IModel channel)
        {
            _channel = channel;
        }

        public void PublishMessage(string exchange, string routingKey, string message)
        {
            _channel.ExchangeDeclare(exchange, ExchangeType.Direct);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
