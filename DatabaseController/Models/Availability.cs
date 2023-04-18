using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Наличие"
    public class Availability
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
