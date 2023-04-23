using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Продукт"
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Composition { get; set; }

        public string Dosage { get; set; }

        public int QuantityPackage { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Availability Availability { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }

        [JsonIgnore]
        public List<OrderProductInfo> OrderProductInfos { get; set; }

        [JsonIgnore]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        [JsonIgnore]
        public int ReleaseFormId { get; set; }
        public ReleaseForm ReleaseForm { get; set; }

        public ICollection<Indication> Indication { get; set; }

        public ICollection<Contraindication> Contraindication { get; set;}

        public ICollection<Review> Review { get; set; }

        [JsonIgnore]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
