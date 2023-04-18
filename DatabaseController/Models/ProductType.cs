using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Тип продукта"
    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
