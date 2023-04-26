using Newtonsoft.Json;

namespace DisServer.Models
{
    public class OrderModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("OrderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("OrderStatus")]
        public string OrderStatus { get; set; }

        [JsonProperty("GrandTotal")]
        public int GrandTotal { get; set; }

        [JsonProperty("ProductModels")]
        public List<OrderProductModel> ProductModels { get; set; }
    }

    public class OrderProductModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public int Price { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; } 

    }
}
