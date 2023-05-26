using Newtonsoft.Json;

namespace DisServer.Models.Desktop
{
    public class PostProductModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Composition")]
        public string Composition { get; set; }

        [JsonProperty("Dosage")]
        public string Dosage { get; set; }

        [JsonProperty("QuantityPackage")]
        public int QuantityPackage { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("Price")]
        public int Price { get; set; }

        [JsonProperty("ProductTypeId")]
        public int ProductTypeId { get; set; }

        [JsonProperty("ReleaseFormId")]
        public int ReleaseFormId { get; set; }

        [JsonProperty("IndicationIds")]
        public List<int> IndicationIds { get; set; }

        [JsonProperty("ContraindicationIds")]
        public List<int> ContraindicationIds { get; set; }

        [JsonProperty("SideEffectIds")]
        public List<int> SideEffectIds { get; set; }

        [JsonProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }

        [JsonProperty("Image")]
        public byte[] Image { get; set; }

        [JsonProperty("ImageName")]
        public string ImageName { get; set; }
    }
}
