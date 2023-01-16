using Assessment.Core.Settings;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        protected ConsumerSettings _settings { get; set; }
        private CancellationTokenSource _cancellationToken { get; set; }
        private Thread _runner;
        protected BaseConsumer(ILogger logger, ConsumerSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }
        public void Start()
        {
            _logger.LogInformation("Consumer Starting..");
            _cancellationToken = new CancellationTokenSource();
            _runner = new Thread(startInternal);
            _runner.Start();
        }
        public void StartWithWaiting()
        {
            _logger.LogInformation("Consumer Starting..");
            _cancellationToken = new CancellationTokenSource();
            startInternal();
        }
        private void startInternal()
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_settings.ConsumerConfiguration)
                         .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}"))
                .Build();

            consumer.Subscribe(_settings.Topic);
            _logger.LogInformation($"Subscribed to {_settings.Topic}");
            while (!_cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(_cancellationToken.Token);

                if (consumeResult?.Message?.Value is string result)
                {
                    Consume(JsonConvert.DeserializeObject<TRequest>(result), _cancellationToken.Token);
                }
            }
            consumer.Close();

        }

        protected abstract void Consume(TRequest request, CancellationToken cancellationToken);
        public void Stop()
        {
            _logger.LogInformation("Consumer Stopping..");
            _cancellationToken.Cancel();
        }
    }
}
