using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Пользователь"
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserInfo UserInfo { get; set; }

        public ICollection<Product> Products { get; set; }

        public List<ShoppingCart> ShoppingCarts { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
