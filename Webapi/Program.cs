using BusinessServices;
using Repository;

namespace Webapi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<ImagesService>();
        builder.Services.AddScoped<CountriesRepository>();
        builder.Services.AddScoped<CountriesService>();
        builder.Services.AddScoped<StatesRepository>();
        builder.Services.AddScoped<StatesService>();
        builder.Services.AddScoped<FriendsRepository>();
        builder.Services.AddScoped<FriendsService>();


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}