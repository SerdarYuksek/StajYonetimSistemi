using RabbitMQ.Client;

namespace RabbitMq
{
    public class RabbitMQConnection
    {
        private static readonly RabbitMQConnection instance = new RabbitMQConnection("amqps://nrywdvoo:dBNHOCYEbEfutKZeisvIrd_KkZ4Lytyd@rat.rmq2.cloudamqp.com/nrywdvoo");
        private readonly IConnection _connection;

        static RabbitMQConnection() { }

        private RabbitMQConnection(string connectionString)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };
            _connection = factory.CreateConnection();
        }

        public static RabbitMQConnection Instance
        {
            get
            {
                return instance;
            }
        }

        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
