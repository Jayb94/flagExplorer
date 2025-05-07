using System.Text.Json.Serialization;

namespace FlagExplorer.Infrastructure.Dtos
{
    public class FlagsDto
    {
        [JsonPropertyName("png")]
        public string Png { get; set; }
    }
}
