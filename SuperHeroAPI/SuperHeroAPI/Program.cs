
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;

namespace SuperHeroAPI
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
            builder.Services.AddCors();
            var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataContext>(
                options =>
                {
                    options.UseSqlServer(connectionstring);
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().WithOrigins
            ("http://localhost:4200"));

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}