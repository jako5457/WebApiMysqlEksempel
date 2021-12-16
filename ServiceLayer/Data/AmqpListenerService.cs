using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceLayer.Pressures;
using ServiceLayer.Tempratures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Data
{
    public class AmqpListenerService : IAmqpListenerService
    {
        private string _Host;
        private string _User;
        private string _Password;

        private IServiceProvider _serviceProvider;

        private EventingBasicConsumer Consumer;
        private IConnection connection;
        private IModel Channel;

        public AmqpListenerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Host { get => _Host; set => _Host = value; }
        public string User { get => _User; set => _User = value; }
        public string Password { get => _Password; set => _Password = value; }

        public void SetConnection(string host, string user, string pass)
        {
            _Host = host;
            _User = user;
            _Password = pass;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = _Host, UserName = _User, Password = _Password };

            connection = factory.CreateConnection();
            Channel = connection.CreateModel();

            Channel.QueueDeclare(queue: "WebApi",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            Channel.QueueBind("", "amq.topic", "house.press");
            Channel.QueueBind("", "amq.topic", "house.temp");

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += Consumer_Received;
            Channel.BasicConsume(queue: "WebApi",
                                 autoAck: true,
                                 consumer: consumer);
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pressureService = scope.ServiceProvider.GetRequiredService<IPressureService>();
                var tempratureSevice = scope.ServiceProvider.GetRequiredService<ITempratureService>();
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                switch (e.RoutingKey)
                {
                    case "house.press":
                        await pressureService.SetAsync(Convert.ToDouble(message));
                        break;
                    case "house.temp":
                        await tempratureSevice.SetAsync(Convert.ToDouble(message));
                        break;
                    default:
                        break;
                }
            }   
        }
    }
}
