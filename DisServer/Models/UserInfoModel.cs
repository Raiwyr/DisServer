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
        public GenderModel Gender { get; set; }
    }

    public class GenderModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
