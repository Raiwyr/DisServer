using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Производители"
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
