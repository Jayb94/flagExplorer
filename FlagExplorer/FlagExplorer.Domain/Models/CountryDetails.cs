namespace FlagExplorer.Domain.Models
{
    public class CountryDetails
    {
        public string Name { get; set; }

        public int Population { get; set; }

        public string Capital { get; set; }

        public string Flag { get; set; }

        public CountryDetails ToDomain(string name, string flag, string capital, int population)
        {
            return new CountryDetails
            {
                Name = name,
                Flag = flag,
                Capital = capital,
                Population = population
            };
        }
    }
}
