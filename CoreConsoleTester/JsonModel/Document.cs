using Newtonsoft.Json;

namespace Birthdays.JsonModel
{
    public class Document
    {

        [JsonProperty("file_name")]
        public string FileName { get; set; }

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
