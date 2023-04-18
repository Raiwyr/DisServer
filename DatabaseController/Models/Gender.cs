using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Пол"
    public class Gender
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<UserInfo> UserInfos { get; set; }
    }
}
