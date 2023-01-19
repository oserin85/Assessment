using Assessment.Core.Helpers;
using Assessment.Core.Settings;
using Assessment.EventServices.Api.Producers;
using Assessment.Models.Service.EventServices;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.IO;

namespace AssessmentTest
{
    public class AssessmentUnitTest
    {
        [Fact]
        public void GetConfiguration()
        {
            var configuration = ConfigurationHelper.GenerateConsumerConfiguration();
            Assert.NotNull(configuration);
            var settings = configuration.GetConfiguration<ConsumerSettings>("ConsumerSetting");
            Assert.NotNull(settings);
            const string expected = "EventTopic";
            Assert.Equal(expected, settings.Topic);
        }

        [Fact]
        public void SendEvent()
        {
            var configuration = ConfigurationHelper.GenerateConsumerConfiguration();
            Assert.NotNull(configuration);
            var settings = configuration.GetConfiguration<ProducerConfig>("ProducerConfiguration");
            Assert.NotNull(settings);
            using var loggerFactory = getLogger();
            ILogger<EventProducer> logger = loggerFactory.CreateLogger<EventProducer>();
            var producer = new EventProducer(settings, logger);
            Assert.NotNull(producer);
            Assert.NotNull(eventData);
            var result = producer.SendMessage(eventData).Result;
            Assert.False(result);
        }

        private ILoggerFactory getLogger()
        {
            return LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
        }
        private Event? eventData
        {
            get
            {
                return JsonConvert.DeserializeObject<Event>(eventJson);
            }
        }

        private string eventJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Event.json"));

    }
}