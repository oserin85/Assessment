using Newtonsoft.Json;

namespace Assessment.Models.Service.Events
{
    public class User
    {
        public bool IsAuthenticated { get; set; }
        public string Provider { get; set; }
        public int Id { get; set; }
        [JsonProperty(PropertyName = "e-mail")]
        public string Email { get; set; }
    }


}
