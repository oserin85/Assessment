using Assessment.Core.Settings;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Core.Base
{
    public abstract class BaseConsumer<TRequest> where TRequest : class
    {
        private ILogger _logger { get; set; }
        private ConsumerSettings _settings { get; set; }
        private CancellationTokenSource _cancellationToken { get; set; }
        private Thread _runner;
        protected BaseConsumer(ILogger logger, ConsumerSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }
        public void Start()
        {
            _logger.Information("Consumer Starting..");
            _cancellationToken = new CancellationTokenSource();
            _runner = new Thread(startInternal);
            _runner.Start();
        }
        private void startInternal()
        {
            using var consumer = new ConsumerBuilder<Ignore, TRequest>(_settings.ConsumerConfiguration)
                .SetValueDeserializer(new JsonDeserializer<TRequest>().AsSyncOverAsync())
                         .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .Build();

            consumer.Subscribe(_settings.Topic);
            Console.WriteLine($"Subscribed to {_settings.Topic}");
            while (_cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(_cancellationToken.Token);

                if (consumeResult?.Message?.Value is TRequest result)
                {
                    Consume(result, _cancellationToken.Token);
                }
            }
            consumer.Close();

        }

        protected abstract void Consume(TRequest request, CancellationToken cancellationToken);
        public void Stop()
        {
            _logger.Information("Consumer Stopping..");
            _cancellationToken.Cancel();
        }
    }
}
