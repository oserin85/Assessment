namespace Assessment.Models.Service.EventServices
{
    public class Event
    {
        public string App { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
        public bool IsSucceeded { get; set; }
        public Meta Meta { get; set; }
        public User User { get; set; }
        public Attributes Attributes { get; set; }
    }


}
