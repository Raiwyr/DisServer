using DatabaseController.Models;
using Newtonsoft.Json;

namespace DisServer.Models
{
    public class UserInfoModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("BirthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }
    }
}
