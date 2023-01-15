using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Settings
{
    public class ConsumerSettings
    {
        public string Topic { get; set; }
        public string ServiceUrl { get; set; }
        public ConsumerConfig ConsumerConfiguration { get; set; }

    }
}
