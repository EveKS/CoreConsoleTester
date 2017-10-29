using Newtonsoft.Json;

namespace Birthdays.JsonModel
{
    public class CallbackRequest
    {

        [JsonProperty("update_id")]
        public int UpdateId { get; set; }

        [JsonProperty("callback_query")]
        public CallbackQuery CallbackQuery { get; set; }
    }
}
