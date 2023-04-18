using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Показание"
    public class Indication
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
