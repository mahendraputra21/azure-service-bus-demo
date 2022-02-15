using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;

namespace SBSender.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _config;
        private const int numOfMessages = 3;

        public QueueService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
        {
            var queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);
            string messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await queueClient.SendAsync(message);
        }
    }
}
