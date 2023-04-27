using Newtonsoft.Json;

namespace DisServer.Models
{
    public class RegistrationModel
    {
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("BirthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("GenderId")]
        public int GenderId { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
