using FarazWare.Application.Contracts.Services;
using FarazWare.Application.UseCases;
using FarazWare.Infrastructure.Clients;
using FarazWare.Infrastructure.Configuration;
using FarazWare.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Uncomment this if using JWT
using Microsoft.AspNetCore.Builder;

namespace FarazWare.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // بارگذاری تنظیمات
            builder.Services.Configure<BearerTokenOptions>(
                builder.Configuration.GetSection("BearerTokenOptions"));
            builder.Services.Configure<AuthSettings>(
                builder.Configuration.GetSection("AuthSettings"));
            builder.Services.Configure<KeySettings>(
                builder.Configuration.GetSection("Keys"));

            // ثبت سرویس‌ها
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IEncryptionService, RsaEncryptionService>();
            builder.Services.AddHttpClient<IAuthClient, AuthClient>();
            builder.Services.AddHttpClient<ICardClient, CardClient>();
            builder.Services.AddTransient<AcquireTokenUseCase>();
            builder.Services.AddTransient<GetCardsUseCase>();
            builder.Services.AddHttpClient<OAuthClient>();
            builder.Services.AddScoped<IClientCredentialsService, ClientCredentialsService>();


            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            // Uncomment to use JWT Bearer Authentication
            /*
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = builder.Configuration["OAuth:Authority"];
                options.Audience = builder.Configuration["OAuth:ClientId"];
                options.RequireHttpsMetadata = true;
            });
            */

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApiDocument();

            var app = builder.Build();

            app.UseOpenApi(); // Serve OpenAPI/Swagger documents

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi(); // Serve Swagger UI
            }

            app.MapGet("/",()=>Results.Redirect("/swagger"));

            app.UseHttpsRedirection();
            // Uncomment if using JWT Bearer Authentication
            // app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
