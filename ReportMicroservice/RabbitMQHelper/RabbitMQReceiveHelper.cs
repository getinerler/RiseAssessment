using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMicroservice.RabbitMQHelper
{
    public class RabbitMQReceiveHelper
    {
        public static void SendNewRequest(Guid guid)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "RiseAssessmentQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonConvert.SerializeObject(new RabbitMqMessage() { Guid = guid });
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "RiseAssessmentQueue",
                                 basicProperties: null,
                                 body: body);
        }

        public static List<RabbitMqMessage> ReceiveQueuedMessages()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            List<RabbitMqMessage> rabbitMessage = new List<RabbitMqMessage>();

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                rabbitMessage.Add(JsonConvert.DeserializeObject<RabbitMqMessage>(message));
            };

            string res = channel.BasicConsume(
                queue: "RiseAssessmentQueue",
                autoAck: true,
                consumer: consumer);

            connection.Dispose();
            channel.Dispose();

            return rabbitMessage;
        }
    }
}
