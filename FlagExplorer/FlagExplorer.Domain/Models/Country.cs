namespace FlagExplorer.Domain.Models
{
    public class Country
    {
        public string Name { get; set; }

        public string Flag { get; set; }

        public Country ToDomain(string name, string flag)
        {
            return new Country
            {
                Name = name,
                Flag = flag
            };
        }
    }
}
