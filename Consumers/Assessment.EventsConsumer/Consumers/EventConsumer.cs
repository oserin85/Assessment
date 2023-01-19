using Assessment.Core.Base;
using Assessment.Core.Settings;
using Assessment.Models.Service.EventServices;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.EventsConsumer.Consumers
{
    internal class EventConsumer : BaseConsumer<Event>
    {
        public EventConsumer(ILogger<EventConsumer> logger, ConsumerSettings settings) : base(logger, settings) { }
        protected override void Consume(Event request, CancellationToken cancellationToken)
        {
            using var client = new RestClient();
            client.UseNewtonsoftJson();
            var restRequest = new RestRequest(new Uri(_settings.ServiceUrl), Method.Post)
            {
                RequestFormat = DataFormat.Json,
            };
            restRequest.AddJsonBody(request);
            var result = client.ExecutePost(restRequest);
        }
    }
}
