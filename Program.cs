using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.Service;
using EmployeeManagementSystem.ServiceFilter;

namespace EmployeeManagementSystem
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

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Register CosmosDB service
            builder.Services.AddSingleton<ICosmosDBService, CosmosDBService>();
            builder.Services.AddScoped<IEmployeeBasicDetailsService, EmployeeBasicDetailsService>();
            builder.Services.AddScoped<BuildEmployeeFilter>();

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
