using Newtonsoft.Json;

namespace DepthChartPro.API.Models
{
    public class AddPlayerModel
    {
        [JsonProperty(PropertyName = "position")]
        public string position { get; set; }
        [JsonProperty(PropertyName = "playerId")]
        public int playerId { get; set; }
        [JsonProperty(PropertyName = "positionDepth")]
        public int? positionDepth { get; set; }
    }
}
