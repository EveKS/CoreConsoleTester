using Newtonsoft.Json;

namespace Birthdays.JsonModel
{
    public class Video
    {

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("thumb")]
        public Thumb Thumb { get; set; }

        [JsonProperty("file_id")]
        public string FileId { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; set; }
    }
}
