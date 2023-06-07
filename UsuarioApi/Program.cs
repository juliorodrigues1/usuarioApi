using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuarioApi.Data;
using UsuarioApi.Models;
using UsuarioApi.Services;

namespace UsuarioApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UsuarioDbContext>
            (opts =>
            {
                opts.UseNpgsql(builder.Configuration.GetConnectionString("DataBase"));
            });

            builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<UsuarioDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<TokenService>();
            
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