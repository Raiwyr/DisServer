﻿using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Заказ"
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        
        public ICollection<Product> Products { get; set; }

        [JsonIgnore]
        public List<OrderProductInfo> OrderProductInfos { get; set; }
    }
}
