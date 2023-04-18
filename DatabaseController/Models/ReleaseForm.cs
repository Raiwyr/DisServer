using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Форма выпуска"
    public class ReleaseForm
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
