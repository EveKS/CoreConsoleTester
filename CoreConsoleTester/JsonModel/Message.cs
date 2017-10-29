using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birthdays.JsonModel
{
    public class Message
    {

        [JsonProperty("message_id")]
        public int MessageId { get; set; }

        [JsonProperty("from")]
        public User From { get; set; }

        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("document")]
        public Document Document { get; set; }

        [JsonProperty("photo")]
        public IList<Photo> Photo { get; set; }

        [JsonProperty("voice")]
        public Voice Voice { get; set; }

        [JsonProperty("audio")]
        public Audio Audio { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("video")]
        public Video Video { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
