namespace FlagExplorer.Infrastructure.Options
{
    public class CountryInfoProviderOptions
    {
        public string BaseAddress { get; set; }

        public string GetAllEndpoint { get; set; }

        public string GetByNameEndpoint { get; set; }
    }
}
