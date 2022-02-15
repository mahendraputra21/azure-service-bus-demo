using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using SBReceiver.Constants;
using SBShared.Models;

namespace SBReciever
{
    class Program
    {
        private static IQueueClient _queueClient;

        static async Task Main(string[] args)
        {
            _queueClient = new QueueClient(Constant.SBReceiverSettings.ConnectionString,
                Constant.SBReceiverSettings.QueueName);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            Console.ReadLine();

            await _queueClient.CloseAsync();
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            ProductModel? productModel = JsonSerializer.Deserialize<ProductModel>(jsonString);
            Console.WriteLine($"Product Received: {jsonString}");
            //Console.WriteLine(
            //    $"Product Received: {productModel?.Name} | {productModel?.Barcode} | {productModel?.Description} | {productModel!.Rate} | {productModel.BuyingPrice}");

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);

            await CreateProductAsync(productModel);

        }

        private static async Task<Uri?> CreateProductAsync(ProductModel product)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri(Constant.SBReceiverSettings.WebApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = await client.PostAsJsonAsync("api/product", product);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler exception: {arg.Exception}");
            return Task.CompletedTask;
        }
    }
}

