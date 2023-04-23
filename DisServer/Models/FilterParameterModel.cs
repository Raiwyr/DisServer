using Newtonsoft.Json;

namespace DisServer.Models
{
    public class FilterParameterModel
    {
        [JsonProperty("ReleaseForms")]
        public List<Parameter> ReleaseForms { get; set; }

        [JsonProperty("Indications")]
        public List<Parameter> Indications { get; set; }

        [JsonProperty("QuantityPackage")]
        public List<int> QuantityPackage { get; set; }

        [JsonProperty("Manufacturers")]
        public List<Parameter> Manufacturers { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
