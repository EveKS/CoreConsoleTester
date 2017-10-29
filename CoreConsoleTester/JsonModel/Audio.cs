using Newtonsoft.Json;

namespace Birthdays.JsonModel
{
    public class Audio
    {

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("performer")]
        public string Performer { get; set; }

        [JsonProperty("file_id")]
        public string FileId { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; set; }
    }
}
