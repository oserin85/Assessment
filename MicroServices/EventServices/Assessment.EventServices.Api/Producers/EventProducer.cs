using Assessment.Core.Base;
using Assessment.Core.Interfaces;
using Assessment.Models.Service.EventServices;
using Confluent.Kafka;

namespace Assessment.EventServices.Api.Producers
{
    public class EventProducer : BaseProducer<Event>, IProducer
    {
        public EventProducer(ProducerConfig config, ILogger<EventProducer> logger) : base(config, logger)
        {
        }

        protected override string TopicName => "EventTopic";
    }
}
