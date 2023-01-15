using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;
using Serilog;

namespace Assessment.Core.Base
{
    public abstract class BaseProducer<T> : IDisposable
                                          where T : class
    {
        protected abstract string TopicName { get; }
        private ProducerConfig _config { get; set; }
        private IProducer<Null, string> _producer;
        private ILogger _logger { get; set; }
        protected BaseProducer(ProducerConfig config, ILogger logger)
        {
            _config = config;
            _producer = new ProducerBuilder<Null, string>(_config).Build();
            _logger = logger;
        }

        public async Task<bool> SendMessage(T message)
        {
            string messageString = JsonConvert.SerializeObject(message);
            try
            {
                var result = await _producer.ProduceAsync(TopicName, new Message<Null, string>
                {
                    Value = messageString
                });
                _logger.Information($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occured: {ex.Message}");
            }

            return false;
        }
        public async Task<bool[]>? SendMessages(List<T> messages)
        {
            return await Task.WhenAll(messages.Select(p => SendMessage(p)));
        }
        public void Dispose()
        {
            if (_producer != null) _producer.Dispose();
        }
    }
}
