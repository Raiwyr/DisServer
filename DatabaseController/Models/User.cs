namespace DatabaseController.Models
{
    //Таблица "Пользователь"
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserInfo UserInfo { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
