using Newtonsoft.Json;

namespace DisServer.Models.Desktop
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

        [JsonProperty("Products")]
        public List<OrderProductModel> Products { get; set; }
    }

    public class OrderProductModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public int Price { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}
