using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Birthdays.JsonModel
{
    public class RequestMessage
    {

        [JsonProperty("update_id")]
        public int UpdateId { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }
}
