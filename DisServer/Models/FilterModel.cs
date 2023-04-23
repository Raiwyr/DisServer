using Newtonsoft.Json;

namespace DisServer.Models
{
    public class FilterModel
    {
        [JsonProperty("MinPrice")]
        public int? MinPrice { get; set; }

        [JsonProperty("MaxPrice")]
        public int? MaxPrice { get; set; }

        [JsonProperty("ReleaseFormsIds")]
        public List<int>? ReleaseFormsIds { get; set; }

        [JsonProperty("IndicationsIds")]
        public List<int>? IndicationsIds { get; set; }

        [JsonProperty("QuantityPackage")]
        public List<int>? QuantityPackage { get; set; }

        [JsonProperty("ManufacturersIds")]
        public List<int>? ManufacturersIds { get; set; }
    }
}
