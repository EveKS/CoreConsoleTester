using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birthdays.JsonModel
{
    public class InlineKeyboardMarkup
    {
        [JsonProperty("inline_keyboard")]
        public IList<List<InlineKeyboardButton>> InlineKeyboard { get; set; }
    }
}
