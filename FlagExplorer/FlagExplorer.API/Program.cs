
using FlagExplorer.Domain.InfrastructureContracts;
using FlagExplorer.Domain.Services;
using FlagExplorer.Infrastructure.Options;
using FlagExplorer.Infrastructure.Providers;

namespace FlagExplorer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add service
            builder.Services.AddTransient<ICountryService, CountryService>();
            builder.Services.AddHttpClient<ICountryInfoProvider, CountryInfoProvider>();

            builder.Services.Configure<CountryInfoProviderOptions>(builder.Configuration.GetSection("CountryInfoProviderOptions"));

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
