using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MALAB3_1.Services
{
    public class RabbitMqService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqService(IConfiguration configuration)
        {
            _configuration = configuration;
            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "test-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public string ReceiveMessages()
        {
            string receivedMessage = null; // Инициализируем переменную для хранения сообщения

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                receivedMessage = Encoding.UTF8.GetString(body); // Присваиваем полученное сообщение переменной
                Console.WriteLine($"Received message: {receivedMessage}");
            };

            _channel.BasicConsume(queue: "test-queue", autoAck: true, consumer: consumer);

            return receivedMessage;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
