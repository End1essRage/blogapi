using BlogApi.Data;
using BlogApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("BlogDbConnection");
            builder.Services.AddDbContext<BlogDbContext>(options => 
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("BlogApi.Api")));

            builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
                
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
}