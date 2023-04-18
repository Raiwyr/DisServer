using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Противопоказания"
    public class Contraindication
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
