using Newtonsoft.Json;

namespace Birthdays.JsonModel
{
    public class Thumb
    {

        [JsonProperty("file_id")]
        public string FileId { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
