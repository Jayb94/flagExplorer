using System.Text.Json.Serialization;

namespace FlagExplorer.Infrastructure.Dtos
{
    public class CountryDto
    {
        [JsonPropertyName("name")]
        public NameDto Name { get; set; }

        [JsonPropertyName("flags")]
        public FlagsDto Flags { get; set; }
    }
}
