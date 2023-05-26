using Newtonsoft.Json;

namespace DisServer.Models.Desktop
{
    public class WorkerModel
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }

        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("IsAdmin")]
        public bool IsAdmin { get; set; }
    }
}
