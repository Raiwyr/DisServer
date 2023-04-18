using Newtonsoft.Json;

namespace DisServer.Models
{
    [Serializable]
    public class SelectionParameterModel
    {
        [JsonProperty("IndicationId")]
        public int? IndicationId { get; set; }

        [JsonProperty("ContraindicationIds")]
        public List<int>? ContraindicationIds { get; set; }

        [JsonProperty("PriseSort")]
        public bool? PriseSort { get; set; }

        [JsonProperty("AssessmentSort")]
        public bool? AssessmentSort { get; set; }

        [JsonProperty("ReviewsSort")]
        public bool? ReviewsSort { get; set; }

        [JsonProperty("CountResults")]
        public int? CountResults { get; set; }

        [JsonProperty("Availability")]
        public bool? Availability { get; set; }


        [JsonProperty("evaluationContraindication")]
        public int? evaluationContraindication { get; set; }

        [JsonProperty("evaluationPrise")]
        public int? evaluationPrise { get; set; }

        [JsonProperty("evaluationAssessment")]
        public int? evaluationAssessment { get; set; }

        [JsonProperty("evaluationReviews")]
        public int? evaluationReviews { get; set; }
    }
}
