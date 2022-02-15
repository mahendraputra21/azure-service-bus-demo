namespace SBReceiver.Constants
{
    public static class Constant
    {
        public static class SBReceiverSettings
        {
            public const string WebApiBaseUrl = "https://localhost:xxxxx/";
            public const string ConnectionString = "Endpoint=sb://timcoservicebus1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<YourKey>";
            public const string QueueName = "personqueue";
            public const int numOfMessages = 3;
        }

       
    }
}
