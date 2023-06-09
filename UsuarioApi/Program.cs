using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsuarioApi.Authorization;
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
            builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();
            builder.Services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kasjdklasndalskdnaldknaldknafjhdjfljksdjflskdfj")),
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization(opts =>
            {
                opts.AddPolicy("IdadeMinima", policy => 
                    policy.AddRequirements(new IdadeMinima(18))
                    );
            });
            
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}