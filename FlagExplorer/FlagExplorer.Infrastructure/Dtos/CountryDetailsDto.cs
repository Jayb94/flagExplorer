using System.Text.Json.Serialization;

namespace FlagExplorer.Infrastructure.Dtos
{
    public class CountryDetailsDto
    {
        [JsonPropertyName("name")]
        public NameDto Name { get; set; }

        [JsonPropertyName("flags")]
        public FlagsDto Flags { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("capital")]
        public string[] Capital { get; set; }
    }
}
