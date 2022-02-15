namespace SBReceiver.Constants
{
    public static class Constant
    {
        public static class SBReceiverSettings
        {
            public const string WebApiBaseUrl = "https://localhost:44383/";
            //public const string WebApiBaseUrl = "https://devproductservice.azurewebsites.net/";
            public const string ConnectionString = "Endpoint=sb://timcoservicebus1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1U7AnYBAN50HF0SHyYO+2lojJ4xMCa3/FGyv7hDyY0E=";
            public const string QueueName = "personqueue";
            public const int numOfMessages = 3;
        }

       
    }
}
