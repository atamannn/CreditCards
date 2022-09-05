using Newtonsoft.Json;

namespace CreditCards.UITests
{
    public class ListSupport
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

}
