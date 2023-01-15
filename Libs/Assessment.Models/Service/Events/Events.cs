using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Models.Service.Events
{
    public class Events
    {
        [JsonProperty(PropertyName = "events")]
        public Event[] EventsList { get; set; }
    }
}
