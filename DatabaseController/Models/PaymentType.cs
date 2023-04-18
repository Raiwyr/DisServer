using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Тип оплаты"
    public class PaymentType
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}
