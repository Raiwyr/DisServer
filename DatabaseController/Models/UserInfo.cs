using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Информация о пользователе"
    public class UserInfo
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
