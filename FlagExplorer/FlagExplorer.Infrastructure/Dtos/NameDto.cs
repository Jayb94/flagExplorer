using System.Text.Json.Serialization;

namespace FlagExplorer.Infrastructure.Dtos
{
    public class NameDto
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }

        [JsonPropertyName("official")]
        public string Official { get; set; }
    }
}
