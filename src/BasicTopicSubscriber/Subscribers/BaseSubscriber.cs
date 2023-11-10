using Azure.Messaging.ServiceBus;

namespace BasicTopicSubscriber.Subscribers
{
    public abstract class BaseSubscriber : IAsyncDisposable
    {
        private readonly ServiceBusProcessor _processor;

        protected BaseSubscriber(ServiceBusClient serviceBusClient,
            string topicName,
            string subscriptionName)
        {
            _processor = serviceBusClient.CreateProcessor(topicName, subscriptionName);

            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += HandleErrorAsync;
        }

        // You can use with or without cancellation tokens
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _processor.StartProcessingAsync(cancellationToken);
        }

        // You can use with or without cancellation tokens
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.CloseAsync(cancellationToken);
        }

        public abstract Task ProcessMessageAsync(ProcessMessageEventArgs args);

        public abstract Task HandleErrorAsync(ProcessErrorEventArgs args);

        public async ValueTask DisposeAsync()
        {
            if (_processor.IsClosed) return;

            await _processor.CloseAsync();
            GC.SuppressFinalize(this);
        }
    }
}
