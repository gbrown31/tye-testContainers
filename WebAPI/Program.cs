
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.BlobStorage;
using Persistence.Database;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI
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

            builder.Services.AddTransient<IFileStorageConfig, FileStorageConfig>();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));
            builder.Services.AddScoped<IDatabase>(a => a.GetService<AppDbContext>());
            builder.Services.AddScoped<IFileStorage, CloudStorage>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
                // DbInitializer.Initialize(context);
            }
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}