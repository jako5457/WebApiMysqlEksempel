using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    public class AmqpService : IHostedService
    {
        private IServiceProvider _serviceProvider;

        private EventingBasicConsumer Consumer;
        private IConnection connection;
        private IModel Channel;

        public AmqpService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            IConfiguration config = _serviceProvider.GetService<IConfiguration>();

            var section = config?.GetSection("Amqp");

            var factory = new ConnectionFactory() {
                HostName = section.GetValue<string>("Hostname"), 
                UserName = section.GetValue<string>("Username"), 
                Password = section.GetValue<string>("Password")
            };

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

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Close();
            return Task.CompletedTask;
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
