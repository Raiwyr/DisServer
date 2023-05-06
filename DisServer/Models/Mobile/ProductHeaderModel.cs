using DatabaseController.Models;
using Newtonsoft.Json;

namespace DisServer.Models.Mobile
{
    public class ProductHeaderModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public int Price { get; set; }

        [JsonProperty("Assessment")]
        public int Assessment { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}
