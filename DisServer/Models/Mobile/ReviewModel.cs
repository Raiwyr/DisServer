using Newtonsoft.Json;

namespace DisServer.Models.Mobile
{
    public class ReviewModel
    {
        [JsonProperty("Assessment")]
        public int Assessment { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("DateReview")]
        public DateTime DateReview { get; set; }

        [JsonProperty("ProductId")]
        public int ProductId { get; set; }
    }
}
