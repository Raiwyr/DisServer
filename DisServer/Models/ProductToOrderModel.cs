using Newtonsoft.Json;

namespace DisServer.Models
{
    public class ProductToOrderModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}
